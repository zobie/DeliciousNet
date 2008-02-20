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


namespace Delicious
{
	internal static class Constants
	{
		/// <summary>
		/// Default url used to access the del.icio.us API
		/// </summary>
		public const string ApiBaseUrl = "https://api.del.icio.us/v1/";

		/// <summary>
		/// Per comment on del.icious.us API site we use an identifiable User-Agent.
		/// The default identifiers like "Java/1.4.3" or "lwp-perl" etc tend to get banned from time to time.
		/// </summary>
		public const string UserAgentValue = @"Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/418.8 (KHTML, like Gecko) Safari/419.3";

		public const int DefaultPostCount = 15;
		public const int MaxPostCount = 100;

        public const int DefaultTimeOut = 20000;

		/// <summary>
		/// Get the minimum number of milliseconds that must elapse between each
		/// query to the del.icio.us servers.
		/// </summary>
		public static int MinimumMillisecondsBetweenQueries
		{
			get
			{
				// During debugging we set this to a higher value than release
				// mode becuase the unit tests likely have a higher chance of
				// being throttled than a normal application using the API.
#if DEBUG
				return 2000;
#else
				return 1000;
#endif
			}
		}


        /// <summary>
        /// Get the maximum number of times that each query will automatically be resent
		/// in cases where the del.icio.us server does not respond as expected.
        /// </summary>
        public const int MaxRetries = 5;


		/// <summary>
		/// Relative urls used to work with the del.icio.us API.
		/// </summary>
		public static class RelativeUrl
		{
			public const string BundlesAll = "tags/bundles/all?";
			public const string BundlesDelete = "tags/bundles/delete?";
			public const string BundlesSet = "tags/bundles/set?";

			public const string InboxGet = "inbox/get?";

			public const string PostsAdd = "posts/add?";
			public const string PostsAll = "posts/all?";
			public const string PostsDelete = "posts/delete?";
			public const string PostsGet = "posts/get?";
			public const string PostsRecent = "posts/recent?";
			public const string PostsUpdate = "posts/update?";

			public const string TagsGet = "tags/get?";
			public const string TagsRename = "tags/rename?";
		}


		/// <summary>
		/// URL parameters for sending data to del.icio.us
		/// </summary>
		public static class UrlParameter
		{
			public const string Bundle = "bundle";
			public const string Count = "count";
			public const string Date = "dt";
			public const string Description = "description";
			public const string Extended = "extended";
			public const string New = "new";
			public const string Old = "old";
			public const string Replace = "replace";
			public const string Shared = "shared";
			public const string Tag = "tag";
			public const string Tags = "tags";
			public const string Url = "url";

			public const string No = "no";
			public const string Yes = "yes";
		}


		/// <summary>
		/// Codes that are returned from del.icio.us
		/// </summary>
		public static class ReturnCode
		{
			public const string Done = "done";
			public const string Ok = "ok";
			public const string SomethingWentWrong = "something went wrong";
		}


		/// <summary>
		/// XmlTags returned from del.icio.us
		/// </summary>
		public static class XmlTag
		{
			public const string Bundle = "bundle";
			public const string Post = "post";
			public const string Result = "result";
			public const string Tag = "tag";
			public const string Update = "update";
		}


		/// <summary>
		/// XmlAttributes returned from del.icio.us
		/// </summary>
		public static class XmlAttribute
		{
			public const string Count = "count";
			public const string Description = "description";
			public const string Extended = "extended";
			public const string Hash = "hash";
			public const string Href = "href";
			public const string Name = "name";
			public const string Shared = "shared";
			public const string Tag = "tag";
			public const string Tags = "tags";
			public const string Time = "time";
		}
	}
}