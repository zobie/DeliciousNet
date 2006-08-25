#region Copyright (c) 2006, Nate Zobrist
/*
 * Copyright (c) 2006, Nate Zobrist
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above
 *     copyright notice, this list of conditions and the following
 *     disclaimer in the documentation and/or other materials provided with
 *     the distribution.
 * Neither the name of "Nate Zobrist" nor the names of its
 *     contributors may be used to endorse or promote products derived from
 *     this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion Copyright (c) 2006, Nate Zobrist

using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace Delicious
{
	public static class Connection
	{
		private static string _Username;
		private static string _Password;
		private static string _ApiBaseUrl;

		private static WebClient webClient;
		private static DateTime lastConnectTime = DateTime.MinValue;


		/// <summary>
		/// Connect to the del.icio.us API
		/// </summary>
		/// <param name="relativeUrl">Constant defined in <c>Delicious.Constants.RelativeUrl</c></param>
		/// <returns>XmlDocument containing data from the del.icio.us api call</returns>
		internal static XmlDocument Connect (string relativeUrl)
		{
			// per instructions on api site, don't allow queries more than once per second
			return Connect (relativeUrl, 1000);
		}

		/// <summary>
		/// Connect to the del.icio.us API
		/// </summary>
		/// <param name="relativeUrl">Constant defined in <c>Delicious.Constants.RelativeUrl</c></param>
		/// <param name="millisecondsBetweenQueries">The amount of time that must pass between queries</param>
		/// <returns>XmlDocument containing data from the del.icio.us api call</returns>
		private static XmlDocument Connect (string relativeUrl, int millisecondsBetweenQueries)
		{
			string fullUrl = ApiBaseUrl + relativeUrl;
			string rawXml;

			TimeSpan span = System.DateTime.Now - lastConnectTime.AddMilliseconds(millisecondsBetweenQueries);
			if (span.TotalMilliseconds < 0)
				Thread.Sleep (Math.Abs((int)span.TotalMilliseconds));

			System.Xml.XmlDocument xmlDoc;
			try
			{
				if (webClient == null)
				{
					webClient = new WebClient ();
					webClient.Headers.Add (HttpRequestHeader.UserAgent, Constants.UserAgentValue);
				}
				if (webClient.Credentials == null)
					webClient.Credentials = new System.Net.NetworkCredential (Username, Password);
				byte[] ba = webClient.DownloadData (fullUrl);
				lastConnectTime = System.DateTime.Now;

				UTF8Encoding encoding = new UTF8Encoding ();
				rawXml = encoding.GetString (ba);

				xmlDoc = new System.Xml.XmlDocument ();
				xmlDoc.LoadXml (rawXml);
			}
			catch (WebException e)
			{
				HttpWebResponse webResponse = e.Response as HttpWebResponse;
				if (webResponse.StatusCode == HttpStatusCode.ServiceUnavailable /*503*/)
				{
					// we have been throttled, increase wait time and try again
					return Connect (relativeUrl, millisecondsBetweenQueries + 1000);
				}
				else if (webResponse.StatusCode == HttpStatusCode.Unauthorized /*401*/)
				{
					throw new Exceptions.DeliciousNotAuthorizedException ("Invalid username/password combination");
				}
				else
				{
					throw new Exceptions.DeliciousException (fullUrl);
				}
			}

			return xmlDoc;
		}


		public static string ApiBaseUrl
		{
			get
			{
				if (_ApiBaseUrl == null || _ApiBaseUrl.Length == 0)
					return Constants.ApiBaseUrl;
				return _ApiBaseUrl;
			}
			set { _ApiBaseUrl = value; }
		}

		public static string Username
		{
			get { return _Username; }
			set
			{
				if (_Username == value)
					return;

				_Username = value;
				if (webClient != null)
					webClient.Credentials = null;
			}
		}

		public static string Password
		{
			get { return _Password; }
			set
			{
				if (_Password == value)
					return;

				_Password = value;
				if (webClient != null)
					webClient.Credentials = null;
			}
		}


		/// <summary>
		/// Gets the time of the last update to the del.icio.us account
		/// </summary>
		/// <returns>Returns a DateTime representing the last update time or DateTime.MinValue if there was an error</returns>
		public static DateTime LastUpdated ()
		{
			DateTime lastUpdated = DateTime.MinValue;

			System.Xml.XmlDocument xmlDoc = Connection.Connect (Constants.RelativeUrl.PostsUpdate);
			if (xmlDoc == null)
				return DateTime.MinValue;

			XmlElement xmlElement = xmlDoc[ Constants.XmlTag.Update ];
			if (xmlElement != null)
			{
				string time = xmlElement.GetAttribute (Constants.XmlAttribute.Time);
				lastUpdated = DateTime.Parse (time);
			}

			return lastUpdated;
		}
	}
}