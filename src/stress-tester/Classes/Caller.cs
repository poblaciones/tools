using System;
using System.Net;
using System.Threading;

namespace stress_tester
{
	public class Caller
	{
		private Client client { get; set; }
		public Synchronized<bool> isCalling;
		private Request request;
		private DateTime startTime;

		public Caller(Client client)
		{
			this.client = client;
			isCalling = new Synchronized<bool>(client.PulseLock);
		}

		public bool TryCall(Request request)
		{
			if (isCalling.Get())
				return false;
			Call(Serializator.Clone(request));
			isCalling.Set(true);
			return true;
		}

		private void Call(Request request)
		{
			this.request = request;
			Current.Context.Results.ExecutingIncrement();
			this.startTime = DateTime.Now;

			Thread t = new Thread(new ParameterizedThreadStart(CallAsync));
			t.Start(request);
		}

		private void CallAsync(object req)
		{
			Request r = (Request) req;
			var response = new Response();
			try
			{
				var httpClient = new WebClient();
				response.Id = r.Id;
				response.StartTime = this.client.GetRelativeTime() + this.client.startDateOffset.TotalMilliseconds;
				var url = r.Url;
//				url = url.Replace("mapa.", "epu.");
				var text = httpClient.DownloadData(url);
				if (text.Length == 0)
					throw new Exception("Empty response");

				Success(response, text.Length);
			}
			catch (Exception e)
			{
				if (e is WebException)
				{
					WebException we = (WebException)e;
					//response.Code = we.Response.ToString();
					response.Code = "9xx";
					if (we.Response is HttpWebResponse)
					{
						HttpWebResponse resp = we.Response as HttpWebResponse;
						response.Code = ((int) resp.StatusCode).ToString() + " " + resp.StatusCode.ToString();
					}
				}
				else
				{
					response.Code = "5xx";
				}
				Failed(response);
			}
		}
		private void Success(Response response, int size)
		{
			response.Code = "200";
			Current.Context.Results.TotalBytes.Increment(size);
			response.Length = size;
			Finished(response);
		}
		private void Failed(Response response)
		{
			if (request.RetryAttemps < Current.Context.Retries)
			{
				Current.Context.Results.Retries.Increment();
				client.ScheduleAsyncRequestRetry(request);
			}
			else
			{
				Current.Context.Results.Errors.Increment();
			}
			Finished(response);
		}
		private void Finished(Response response)
		{
			DateTime now = DateTime.Now;
			Current.Context.Results.Hits.Increment();
			double ellapsed = now.Subtract(startTime).TotalMilliseconds;
			Current.Context.Results.TotalTime.Increment((int) ellapsed);
			response.Ellapsed = ellapsed;

			Current.Context.Results.Executing.Decrement();
			Current.Context.Results.Responses.Add(response);
			isCalling.Set(false);
			client.Pulse();
		}
	}
}
