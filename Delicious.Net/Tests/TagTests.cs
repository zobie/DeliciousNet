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

using Delicious.Exceptions;

using NUnit.Framework;

#if DEBUG
namespace Delicious.Tests
{
	[TestFixture]
	public class TagTests : TestBase
	{
		[Test]
		public void ObjectEquality ()
		{
			Tag t1 = new Tag();
			t1.Name = GetRandomUrl();
			t1.Count = 42;

			Tag t2 = new Tag (t1.Name, t1.Count);

			Assert.IsTrue (t1 == t2, "(t1 == t2) should be true");
			Assert.IsFalse (t1 != t2, "(t1 != t2) should be false");
			Assert.IsTrue (t1.Equals (t2), "t1.Equals(t2) should be true");

			Assert.IsTrue (t1.ReferenceEquals (t1), "t1.ReferenceEquals(t1) should be true");
			Assert.IsFalse (t1.ReferenceEquals (t2), "t1.ReferenceEquals(t2) should be false");
		}


		[Test]
		public void Get ()
		{
			string tag = GetRandomString();
			PostTests.AddTestPost (tag);

			List<Tag> tags = Tag.Get();

			bool found = false;
			foreach (Tag t in tags)
			{
				if (t.Name == tag)
				{
					found = true;
					break;
				}
			}

			Assert.IsTrue (found, "The tag '" + tag + "' was not sucessfully returned.");
		}


		[Test]
		public void Rename ()
		{
			string tag = GetRandomString();
			string newTag = GetRandomString();
			string url = PostTests.AddTestPost (tag);

			Post p = Post.GetPost (url);
			Assert.IsTrue (p.Tag == tag, "The post was not sucessfully created with the tag");

			Tag.Rename (tag, newTag);
			p = Post.GetPost (url);
			Assert.IsTrue (p.Tag == newTag, "The tag was NOT sucessfully renamed");
		}
	}
}
#endif