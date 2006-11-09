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


namespace Delicious
{
	internal static class Constants
	{
		/// <summary>
		/// Default url used to access the del.icio.us API
		/// </summary>
        internal const string ApiBaseUrl = "https://api.del.icio.us/v1/";

		/// <summary>
		/// Per comment on del.icious.us API site we use an identifiable User-Agent.
		/// The default identifiers like "Java/1.4.3" or "lwp-perl" etc tend to get banned from time to time.
		/// </summary>
        internal const string UserAgentValue = @"Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/418.8 (KHTML, like Gecko) Safari/419.3";

        internal const int DefaultPostCount = 15;
        internal const int MaxPostCount = 100;

        /// <summary>
        /// The default http timeout for a connection to the del.icio.us servers
        /// </summary>
        internal const int DefaultTimeOut = 20000;

        /// <summary>
        /// How many times should we retry the delicious servers if we get a timeout error
        /// </summary>
        internal const int MaxRetries = 5;


		/// <summary>
		/// Relative urls used to work with the del.icio.us API.
		/// </summary>
        internal static class RelativeUrl
		{
			internal const string BundlesAll = "tags/bundles/all?";
			internal const string BundlesDelete = "tags/bundles/delete?";
			internal const string BundlesSet = "tags/bundles/set?";

			internal const string InboxGet = "inbox/get?";

			internal const string PostsAdd = "posts/add?";
			internal const string PostsAll = "posts/all?";
			internal const string PostsDelete = "posts/delete?";
			internal const string PostsGet = "posts/get?";
			internal const string PostsRecent = "posts/recent?";
			internal const string PostsUpdate = "posts/update?";

			internal const string TagsGet = "tags/get?";
			internal const string TagsRename = "tags/rename?";
		}


		/// <summary>
		/// URL parameters for sending data to del.icio.us
		/// </summary>
        internal static class UrlParameter
		{
			internal const string Bundle = "bundle";
			internal const string Count = "count";
			internal const string Date = "dt";
			internal const string Description = "description";
			internal const string Extended = "extended";
			internal const string New = "new";
			internal const string Old = "old";
			internal const string Replace = "replace";
			internal const string Shared = "shared";
			internal const string Tag = "tag";
			internal const string Tags = "tags";
			internal const string Url = "url";

			internal const string No = "no";
			internal const string Yes = "yes";
		}


		/// <summary>
		/// Codes that are returned from del.icio.us
		/// </summary>
        internal static class ReturnCode
		{
			internal const string Done = "done";
			internal const string Ok = "ok";
			internal const string SomethingWentWrong = "something went wrong";
		}


		/// <summary>
		/// XmlTags returned from del.icio.us
		/// </summary>
        internal static class XmlTag
		{
			internal const string Bundle = "bundle";
			internal const string Post = "post";
			internal const string Result = "result";
			internal const string Tag = "tag";
			internal const string Update = "update";
		}


		/// <summary>
		/// XmlAttributes returned from del.icio.us
		/// </summary>
        internal static class XmlAttribute
		{
			internal const string Count = "count";
			internal const string Description = "description";
			internal const string Extended = "extended";
			internal const string Hash = "hash";
			internal const string Href = "href";
			internal const string Name = "name";
			internal const string Shared = "shared";
			internal const string Tag = "tag";
			internal const string Tags = "tags";
			internal const string Time = "time";
		}
	}
}