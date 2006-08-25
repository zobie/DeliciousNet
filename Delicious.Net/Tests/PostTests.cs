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
using System.Collections.Generic;

using Delicious.Exceptions;

using NUnit.Framework;

namespace Delicious.Tests
{
	[TestFixture]
	public class PostTests : TestBase
	{
		[Test]
		public void ObjectEquality ()
		{
			Post p1 = new Post();
			p1.Href = this.GetRandomUrl();
			p1.Description = p1.Href;

			Post p2 = new Post (p1.Href, p1.Description, p1.Hash, p1.Tag, p1.Time, p1.Extended, p1.Shared);

			Assert.IsTrue (p1 == p2, "(p1 == p2) should be true");
			Assert.IsFalse (p1 != p2, "(p1 != p2) should be false");
			Assert.IsTrue (p1.Equals (p2), "p1.Equals(p2) should be true");

			Assert.IsTrue (p1.ReferenceEquals (p1), "p1.ReferenceEquals(p1) should be true");
			Assert.IsFalse (p1.ReferenceEquals (p2), "p1.ReferenceEquals(p2) should be false");
		}


		[Test]
		public void AddPost ()
		{
			string url = this.GetRandomUrl();
			string description = url;
			bool added = Post.Add (url, description);
			CleanupPostList.Add (url);

			Assert.IsTrue (added, "The url was NOT sucessfully added to del.icio.us (" + url + ")");
		}


		[Test]
		public void Get ()
		{
			string url = this.AddNewUrlToDelicious ();
			List<Post> posts = Post.Get();
			Assert.IsTrue (posts.Count > 0, "At least one post (" + url + ") should have been returned.");
		}


		[Test]
		public void GetPost ()
		{
			string url = this.AddNewUrlToDelicious ();
			Post p = Post.GetPost (url);
			Assert.IsTrue (p != null && p.Href == url,
			               "The url (" + url + ") was not sucessfully retrieved.");
		}


		[Test]
		public void GetRecentPosts0 ()
		{
			string url = this.AddNewUrlToDelicious ();

			bool found = false;
			List<Post> posts = Post.GetRecentPosts();
			foreach (Post p in posts)
			{
				if (p.Href == url)
				{
					found = true;
					break;
				}
			}

			Assert.IsTrue (found, "GetRecentPosts did not return the newly added url " + url);
		}


		[Test]
		public void GetRecentPosts1 ()
		{
			string tag1 = "tag1";
			string tag2 = "tag2";
			string url = this.AddNewUrlToDelicious (tag1 + " " + tag2);

			bool found = false;
			List<Post> posts = Post.GetRecentPosts (tag1);
			foreach (Post p in posts)
			{
				if (p.Href == url)
				{
					found = true;
					break;
				}
			}
			Assert.IsTrue (found, "GetRecentPosts (when filtered by tag1 = '" + tag1 + "') did not return the newly added url " + url);

			found = false;
			posts = Post.GetRecentPosts (tag2);
			foreach (Post p in posts)
			{
				if (p.Href == url)
				{
					found = true;
					break;
				}
			}
			Assert.IsTrue (found, "GetRecentPosts (when filtered by tag1 = '" + tag2 + "') did not return the newly added url " + url);
		}


		[Test]
		public void DeletePost ()
		{
			string url = this.GetRandomUrl();
			string description = url;
			bool added = Post.Add (url, description);
			CleanupPostList.Add (url);
			Assert.IsTrue (added, "The url was NOT sucessfully added to del.icio.us (" + url + ")");

			bool deleted = Post.Delete (url);
			Assert.IsTrue (deleted, "The DeletePost method did not return a valid status.");
			Post post = Post.GetPost (url);
			Assert.IsNull (post, "The url was NOT sucessfully deleted from del.icio.us (" + url + ")");
		}
	}
}