#region Copyright (c) 2006-2008, Nate Zobrist


/*
 * Copyright (c) 2006-2008, Nate Zobrist
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


#endregion Copyright (c) 2006-2008, Nate Zobrist


using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
		private static int _MaxRetries = Constants.MaxRetries;
		private static int _TimeOut = Constants.DefaultTimeOut;
		private static string _ApiBaseUrl;

		private static DateTime lastConnectTime = DateTime.MinValue;


		/// <summary>
		/// Connect to the del.icio.us API, returning an <c>XmlDocument</c> of the response
		/// </summary>
		/// <param name="relativeUrl">Constant defined in <c>Delicious.Constants.RelativeUrl</c></param>
		/// <returns>XmlDocument containing data from the del.icio.us api call</returns>
		internal static XmlDocument Connect (string relativeUrl)
		{
			System.Xml.XmlDocument xmlDoc;
			try
			{
				xmlDoc = new System.Xml.XmlDocument();
				xmlDoc.LoadXml (GetRawXml (relativeUrl));
			}
			catch (XmlException)
			{
				throw new Exceptions.DeliciousException ("The webserver did not return valid XML.");
			}

			return xmlDoc;
		}


		/// <summary>
		/// Connect to the del.icio.us API, returning the raw xml response
		/// </summary>
		/// <param name="relativeUrl">Constant defined in <c>Delicious.Constants.RelativeUrl</c></param>
		/// <returns>string containing raw xml data from the del.icio.us api call</returns>
		internal static string GetRawXml (string relativeUrl)
		{
			return GetRawXml (relativeUrl, 0);
		}


		/// <summary>
		/// Connect to the del.icio.us API.
		/// Per instructions on the api site, don't allow queries more than once per second.
		/// </summary>
		/// <param name="relativeUrl">Constant defined in <c>Delicious.Constants.RelativeUrl</c></param>
		/// <param name="callCount">Beginning at 0, this number should be incremented by one each time the method
		/// is recursively called due to a Timeout error.  The increased number will increase the forced delay
		/// in contacting the del.icio.us servers.</param>
		/// <returns>XmlDocument containing data from the del.icio.us api call</returns>
		private static string GetRawXml (string relativeUrl, int callCount)
		{
			int millisecondsBetweenQueries = Constants.MinimumMillisecondsBetweenQueries +
			                                 (1000 * callCount * callCount);

			string fullUrl = ApiBaseUrl + relativeUrl;
			string rawXml;

			TimeSpan span = System.DateTime.Now - lastConnectTime.AddMilliseconds (millisecondsBetweenQueries);
			if (span.TotalMilliseconds < 0)
				Thread.Sleep (Math.Abs ((int)span.TotalMilliseconds));

			HttpWebResponse response = null;
			StreamReader readStream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create (fullUrl);
                request.Credentials = new System.Net.NetworkCredential (Username, Password);
                request.Timeout = TimeOut;
                request.AllowAutoRedirect = false;
                request.UserAgent = Constants.UserAgentValue;
                request.MaximumResponseHeadersLength = 4;
                request.KeepAlive = false;
                request.Pipelined = false;
                response = (HttpWebResponse)request.GetResponse ();
                Stream receiveStream = response.GetResponseStream ();
                readStream = new StreamReader (receiveStream, Encoding.UTF8);
                rawXml = readStream.ReadToEnd ();

                lastConnectTime = System.DateTime.Now;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError ||
                    e.Status == WebExceptionStatus.Timeout)
                {
                    HttpWebResponse webResponse = e.Response as HttpWebResponse;
                    if (webResponse != null)
                    {
                        // del.icio.us servers seem to have less-than-ideal response times quite often.
                        // we try to compensate for those probelms, but eventually error out.
                        if (e.Status == WebExceptionStatus.Timeout ||
                            webResponse.StatusCode == HttpStatusCode.ServiceUnavailable /*503 we have been throttled*/||
                            (int)webResponse.StatusCode == 999 /*Unable to process request at this time*/)
                        {
                            if (callCount < MaxRetries) // don't loop endlessly here, eventually just error out
                                return GetRawXml (relativeUrl, callCount + 1);
                            else
                                throw new Exceptions.DeliciousTimeoutException (String.Format ("The server is not responding.\t{0}", webResponse.StatusCode));
                        }
                        else if (webResponse.StatusCode == HttpStatusCode.Unauthorized /*401*/)
                        {
                            throw new Exceptions.DeliciousNotAuthorizedException ("Invalid username/password combination");
                        }
                        else
                        {
                            throw new Exceptions.DeliciousException (webResponse.StatusCode.ToString ());
                        }
                    }
                }

                throw new Exceptions.DeliciousException (e.Status.ToString ());
            }
            catch (IOException e)
            {
                // Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host.
                throw new Exceptions.DeliciousTimeoutException (e.ToString ());
            }
            finally
			{
				if (response != null)
					response.Close();
				if (readStream != null)
					readStream.Close();
			}

			return rawXml;
		}


		/// <summary>
		/// Gets or sets the API base URL.
		/// </summary>
		/// <value>The API base URL.</value>
		public static string ApiBaseUrl
		{
			get
			{
				if (_ApiBaseUrl == null || _ApiBaseUrl.Length == 0)
					return Constants.ApiBaseUrl;
				return _ApiBaseUrl;
			}
			set
            {
                _ApiBaseUrl = value;
                // a bunch of stuff depeneds on this already having the trailing slash
                if (!_ApiBaseUrl.EndsWith ("/"))
                    _ApiBaseUrl += "/";
            }
		}

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The username.</value>
		public static string Username
		{
			get { return _Username; }
			set
			{
				if (_Username == value)
					return;

				_Username = value;
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public static string Password
		{
			get { return _Password; }
			set
			{
				if (_Password == value)
					return;

				_Password = value;
			}
		}


		/// <summary>
		/// Gets or sets the max retries.
		/// </summary>
		/// <value>The max retries.</value>
		public static int MaxRetries
		{
			get { return _MaxRetries; }
			set { _MaxRetries = (value < 0) ? 0 : value; }
		}


		/// <summary>
		/// Gets or sets the time out.
		/// </summary>
		/// <value>The time out.</value>
		public static int TimeOut
		{
			get { return _TimeOut; }
			set { _TimeOut = (value < 0) ? Constants.DefaultTimeOut : value; }
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
                try
                {
                    lastUpdated = DateTime.Parse (time, CultureInfo.InvariantCulture);
                }
                catch (FormatException e)
                {
                    Debug.Fail (e.ToString ());
                }
			}

			return lastUpdated;
		}
	}
}