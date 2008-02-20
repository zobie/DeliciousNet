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
using System.Collections.Generic;
using System.Xml;

namespace Delicious
{
	public class Post
	{
		private string _Href;
		private string _Description;
		private string _Hash;
		private string _Tag;
		private string _Time;
		private string _Extended;
		private bool _Shared;


		public Post ()
		{
		}


		public Post (string href, string description, string hash, string tag, string time, string extended, bool shared)
		{
			this.Href = href;
			this.Description = description;
			this.Hash = hash;
			this.Tag = tag;
			this.Time = time;
			this.Extended = extended;
			this.Shared = shared;
		}


		/// <summary>
		/// Get/Set the Href for this <c>Post</c>
		/// </summary>
		public string Href
		{
			get { return this._Href; }
			set { this._Href = value; }
		}

		/// <summary>
		/// Get/Set the Description of this <c>Post</c>
		/// </summary>
		public string Description
		{
			get { return this._Description; }
			set { this._Description = value; }
		}

		/// <summary>
		/// Get/Set the Hash for this <c>Post</c>
		/// </summary>
		public string Hash
		{
			get { return this._Hash; }
			set { this._Hash = value; }
		}

		/// <summary>
		/// Get/Set the Tag for this <c>Post</c>
		/// </summary>
		public string Tag
		{
			get { return this._Tag; }
			set { this._Tag = value; }
		}

		/// <summary>
		/// Get an array of strings containing each Tag for this <c>Post</c>
		/// </summary>
		public string[] Tags
		{
			get
			{
				if (this.Tag == null || this.Tag.Length == 0)
					return null;
				return this.Tag.Split (' ');
			}
		}

		/// <summary>
		/// Get/Set the Time of this <c>Post</c>
		/// </summary>
		public string Time
		{
			get { return this._Time; }
			set { this._Time = value; }
		}

		/// <summary>
		/// Get/Set the Extended properties for this <c>Post</c>
		/// </summary>
		public string Extended
		{
			get { return this._Extended; }
			set { this._Extended = value; }
		}

		/// <summary>
		/// Get/Set the Shared bit for this <c>Post</c>
		/// </summary>
		public bool Shared
		{
			get { return this._Shared; }
			set { this._Shared = value; }
		}


		/// <summary>
		/// Returns a string that represents the <c>Post</c>
		/// </summary>
		public override string ToString ()
		{
			return this.Href + " : " +
			       this.Description + " : " +
			       this.Hash + " : " +
			       this.Tag + " : " +
			       this.Time + " : " +
			       this.Extended + " : " +
			       this.Shared + " : ";
		}


		public override bool Equals (object obj)
		{
			if (!(obj is Post))
				return false;

			return (this == (Post)obj);
		}

		
		public override int GetHashCode ()
		{
			return this.Href.GetHashCode();
		}


		/// <summary>
		/// Determines whether the specified object instances are the same instance
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool ReferenceEquals (object obj)
		{
			return Object.ReferenceEquals (this, obj);
		}


		#region Static Methods


		public static bool operator == (Post post1, Post post2)
		{
			if ((object)post1 == null)
				return ((object)post2 == null);
			else if ((object)post2 == null)
				return false;

			return (post1.Href == post2.Href &&
			        post1.Description == post2.Description &&
			        post1.Hash == post2.Hash &&
			        post1.Tag == post2.Tag &&
			        post1.Time == post2.Time &&
			        post1.Extended == post2.Extended &&
			        post1.Shared == post2.Shared);
		}


		public static bool operator != (Post post1, Post post2)
		{
			return !(post1 == post2);
		}

		
		/// <summary>
		/// Add a post to del.icio.us
		/// </summary>
		/// <param name="url">URL to be posted</param>
		/// <param name="description">Description of the post</param>
		/// <returns><c>true</c> if posted, <c>false</c> otherwise</returns>
		public static bool Add (string url, string description)
		{
			return Add (url, description, null, null, null, false, true);
		}


		/// <summary>
		/// Add a post to del.icio.us
		/// </summary>
		/// <param name="url">URL to be posted</param>
		/// <param name="description">Description of the post</param>
		/// <param name="extended">Extended for the post</param>
		/// <param name="tags">Space seperated list of tags</param>
		/// <param name="date">Date for the post</param>
		/// <returns><c>true</c> if posted, <c>false</c> otherwise</returns>
		public static bool Add (string url, string description, string extended, string tags, string date)
		{
			return Add (url, description, extended, tags, date, false, true);
		}


		/// <summary>
		/// Add a post to del.icio.us.
		/// If either the <param>url</param> or <param>description</param> parameters are null or empty <c>false</c> is returned immediately.
		/// </summary>
		/// <param name="url">URL to be posted</param>
		/// <param name="description">Description of the post</param>
		/// <param name="extended">Extended for the post</param>
		/// <param name="tags">Space seperated list of tags</param>
		/// <param name="date">Date for the post</param>
		/// <param name="replace">Should the post be replaced if it has already been posted?</param>
		/// <param name="shared">Mark the post as private</param>
		/// <returns>
		/// <c>true</c> if posted, <c>false</c> otherwise.
		/// Note that <c>false</c> will be returned if the url has already been posted.
		/// </returns>
		public static bool Add (string url, string description, string extended, string tags, string date, bool replace, bool shared)
		{
			if (url == null || url.Length == 0 ||
			    description == null || description.Length == 0)
			{
				return false;
			}

			string relativeUrl = Constants.RelativeUrl.PostsAdd;
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Url, url);
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Description, description);
			if (extended != null && extended.Length > 0)
				relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Extended, extended);
			if (tags != null && tags.Length > 0)
				relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Tags, tags);
			if (date != null && date.Length > 0) // TODO: parse date to make sure it is valid
				relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Date, date);
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Replace,
												 ((replace) ? Constants.UrlParameter.Yes : Constants.UrlParameter.No));
			if (!shared)
				relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Shared, Constants.UrlParameter.No);

			XmlDocument xmlDoc = Connection.Connect (relativeUrl);
			string resultCode = Utilities.ParseForResultCode (xmlDoc.DocumentElement);
			return (resultCode == Constants.ReturnCode.Done);
		}


		/// <summary>
		/// Delete the specified post.
		/// </summary>
		/// <param name="url">URL of the post to delete</param>
		/// <returns><c>true</c> if the post was deleted, <c>false</c> otherwise</returns>
		public static bool Delete (string url)
		{
			if (url == null || url.Length == 0)
				return false;

			string relativeUrl = Constants.RelativeUrl.PostsDelete;
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Url, url);

			XmlDocument xmlDoc = Connection.Connect (relativeUrl);
			string resultCode = Utilities.ParseForResultCode (xmlDoc.DocumentElement);
			return (resultCode == Constants.ReturnCode.Done);
		}


		/// <summary>
		/// Return a list of all <c>Post</c> objects.
		/// </summary>
		/// <returns>List of all <c>Post</c> objects</returns>
		public static List<Post> Get ()
		{
			return Get (null);
		}


		/// <summary>
		/// Return the complete list of posts from del.icio.us servers exactly
		/// as delivered from the del.icio.us API (raw xml)
		/// </summary>
		/// <returns>a <c>string</c> of raw xml data</returns>
		public static string GetRawXml ()
		{
			string relativeUrl = Constants.RelativeUrl.PostsAll;
			return Connection.GetRawXml (relativeUrl);
		}


		/// <summary>
		/// Return a list of all <c>Post</c> objects filtered by tag
		/// </summary>
		/// <param name="filterTag">Filter by this tag</param>
		/// <returns>List of <c>Post</c> objects</returns>
		public static List<Post> Get (string filterTag)
		{
			string relativeUrl = Constants.RelativeUrl.PostsAll;
			if (filterTag != null && filterTag.Length > 0)
				relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Tag, filterTag);

			XmlDocument xmlDoc = Connection.Connect (relativeUrl);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Post);

			return ParsePosts (nodeList);
		}


		/// <summary>
		/// Return a list of all <c>Post</c> objects filtered by date
		/// </summary>
		/// <param name="date">Filter by this date</param>
		/// <returns>List of <c>Post</c> objects</returns>
		public static List<Post> Get (DateTime date)
		{
			return Get (null, date, null);
		}


		/// <summary>
		/// Return an ArrayList of <c>Post</c> objects
		/// </summary>
		/// <param name="filterTag">Filter by this tag</param>
		/// <param name="date">Filter by this date (<c>DateTime.MinValue</c> causes this parameter to NOT be used)</param>
		/// <param name="url">URL of the post to retrieve (return a single <c>Post</c> object)</param>
		/// <returns>ArrayList of <c>Post</c> objects</returns>
		public static List<Post> Get (string filterTag, DateTime date, string url)
		{
			string api = Constants.RelativeUrl.PostsGet;
			if (filterTag != null && filterTag.Length > 0)
				api = Utilities.AddParameter (api, Constants.UrlParameter.Tag, filterTag);
			if (date != DateTime.MinValue)
				api = Utilities.AddParameter (api, Constants.UrlParameter.Date, date.ToUniversalTime().ToString());
			if (url != null && url.Length > 0)
				api = Utilities.AddParameter (api, Constants.UrlParameter.Url, url);
			System.Xml.XmlDocument xmlDoc = Connection.Connect (api);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Post);
			return ParsePosts (nodeList);
		}


		/// <summary>
		/// Return a <c>Post</c> object
		/// </summary>
		/// <param name="url">Url of the <c>Post</c> to return</param>
		/// <returns><c>Post</c></returns>
		public static Post GetPost (string url)
		{
			if (url == null || url.Length == 0)
				return null;

			List<Post> posts = Get (null, DateTime.MinValue, url);
			if (posts.Count == 1)
				return posts[ 0 ];

			return null;
		}


		/// <summary>
		/// Return the list of <c>Post</c> items that are in your Inbox
		/// </summary>
		/// <returns>A list of <c>Post</c> objects from your Inbox.</returns>
		public static List<Post> GetPostsInInbox ()
		{
			return GetPostsInInbox (DateTime.MinValue);
		}


		/// <summary>
		/// Return the list of <c>Post</c> items that are in your Inbox
		/// </summary>
		/// <param name="date">Filter by this date</param>
		/// <returns>A list of <c>Post</c> objects from your Inbox.</returns>
		public static List<Post> GetPostsInInbox (DateTime date)
		{
			string relativeUrl = Constants.RelativeUrl.InboxGet;
			if (date != DateTime.MinValue)
				relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Date, date.ToUniversalTime().ToString());

			XmlDocument xmlDoc = Connection.Connect (relativeUrl);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Post);

			return ParsePosts (nodeList);
		}


		/// <summary>
		/// Return a list of <c>Post</c> objects which were recently posted to del.icio.us.
		/// Uses the Constant.DEFAULT_POST_COUNT to determin how many items to retreive.
		/// </summary>
		/// <returns>A list of recent <c>Post</c> objects.</returns>
		public static List<Post> GetRecentPosts ()
		{
			return GetRecentPosts (null, Constants.DefaultPostCount);
		}


		/// <summary>
		/// Return a list of <c>Post</c> objects which were recently posted to del.icio.us.
		/// Uses the Constant.DEFAULT_POST_COUNT to determin how many items to retreive.
		/// </summary>
		/// <param name="filterTag">Filter by this tag (optional)</param>
		/// <returns>A list of recent <c>Post</c> objects.</returns>
		public static List<Post> GetRecentPosts (string filterTag)
		{
			return GetRecentPosts (filterTag, Constants.DefaultPostCount);
		}


		/// <summary>
		/// Return a list of <c>Post</c> objects which were recently posted to del.icio.us.
		/// Uses the Constant.DEFAULT_POST_COUNT to determin how many items to retreive.
		/// </summary>
		/// <param name="filterTag">Filter by this tag (optional)</param>
		/// <param name="count">Must be &gt; 0 and &lt; 100</param>
		/// <returns>A list of recent <c>Post</c> objects.</returns>
		public static List<Post> GetRecentPosts (string filterTag, int count)
		{
			if (count <= 0)
				count = Constants.DefaultPostCount;
			if (count > Constants.MaxPostCount)
				count = Constants.MaxPostCount;

			string relativeUrl = Constants.RelativeUrl.PostsRecent;
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Count, count.ToString());
			if (filterTag != null && filterTag.Length > 0)
				relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Tag, filterTag);
			XmlDocument xmlDoc = Connection.Connect (relativeUrl);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Post);

			return ParsePosts (nodeList);
		}


		private static List<Post> ParsePosts (XmlNodeList nodeList)
		{
			List<Post> posts = new List<Post> (nodeList.Count);

            foreach (XmlNode node in nodeList)
            {
                string href = node.Attributes[ Constants.XmlAttribute.Href ].Value;
                string description = node.Attributes[ Constants.XmlAttribute.Description ].Value;
                string hash = node.Attributes[ Constants.XmlAttribute.Hash ].Value;
                string tag = node.Attributes[ Constants.XmlAttribute.Tag ].Value;
                string time = node.Attributes[ Constants.XmlAttribute.Time ].Value;
                string extended = (node.Attributes[ Constants.XmlAttribute.Extended ] == null) ? String.Empty : node.Attributes[ Constants.XmlAttribute.Extended ].Value;
                bool shared = true;
                if (node.Attributes[ Constants.XmlAttribute.Shared ] != null)
                    shared = (node.Attributes[ Constants.XmlAttribute.Shared ].Value != Constants.UrlParameter.No);

                Post post = new Post (href, description, hash, tag, time, extended, shared);
                posts.Add (post);
            }
			return posts;
		}


		#endregion Static Methods
	}
}