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
	public class BundleTests : TestBase
	{
		[Test]
		public void ObjectEquality ()
		{
			Bundle b1 = new Bundle();
			b1.Name = this.GetRandomUrl();
			b1.Tags = this.GetRandomString() + " " + this.GetRandomString();

			Bundle b2 = new Bundle (b1.Name, b1.Tags);

			Assert.IsTrue (b1 == b2, "(b1 == b2) should be true");
			Assert.IsFalse (b1 != b2, "(b1 != b2) should be false");
			Assert.IsTrue (b1.Equals (b2), "b1.Equals(b2) should be true");

			Assert.IsTrue (b1.ReferenceEquals (b1), "b1.ReferenceEquals(b1) should be true");
			Assert.IsFalse (b1.ReferenceEquals (b2), "b1.ReferenceEquals(b2) should be false");
		}


		[Test]
		public void Add ()
		{
			string tag1 = this.GetRandomString();
			this.AddNewUrlToDelicious (tag1);

			string tag2 = this.GetRandomString();
			this.AddNewUrlToDelicious (tag2);

			string bundleName = this.GetRandomString();
			bool added = Bundle.Add (bundleName, tag1 + " " + tag2);
			Assert.IsTrue (added, "The Bundle does not seem to have been sucessfully added.");
		}


		[Test]
		public void Delete ()
		{
			string tag = this.GetRandomString();
			this.AddNewUrlToDelicious (tag);

			string bundleName = this.GetRandomString();
			Bundle.Add (bundleName, tag);

			bool found = false;
			List<Bundle> bundles = Bundle.Get();
			foreach (Bundle b in bundles)
			{
				if (b.Name == bundleName)
				{
					found = true;
					break;
				}
			}
			Assert.IsTrue (found, "The Bundle '" + bundleName + "' was not sucessfully added");

			Bundle.Delete (bundleName);
			found = false;
			bundles = Bundle.Get();
			foreach (Bundle b in bundles)
			{
				if (b.Name == bundleName)
				{
					found = true;
					break;
				}
			}
			Assert.IsTrue (!found, "The Bundle '" + bundleName + "' was not sucessfully deleted");
		}


		[Test]
		public void Get ()
		{
			string tag1 = this.GetRandomString() + "#?%&";
			this.AddNewUrlToDelicious (tag1);

			string tag2 = this.GetRandomString();
			this.AddNewUrlToDelicious (tag2);

			string bundleName = this.GetRandomString();
			Bundle.Add (bundleName, tag1 + " " + tag2);

			bool found = false;
			bool tag1InBundle = false;
			bool tag2InBundle = false;
			List<Bundle> bundles = Bundle.Get();
			foreach (Bundle b in bundles)
			{
				if (b.Name == bundleName)
				{
					found = true;
					string[] tags = b.Tags.Split (' ');
					foreach (string tag in tags)
					{
						if (tag == tag1)
							tag1InBundle = true;
						else if (tag == tag2)
							tag2InBundle = true;
					}
					break;
				}
			}
			Assert.IsTrue (found, "The Bundle '" + bundleName + "' was not sucessfully returned");
			Assert.IsTrue (tag1InBundle, "Tag1 '" + tag1 + "' was not returned in Bundle.Tag");
			Assert.IsTrue (tag2InBundle, "Tag2 '" + tag2 + "' was not returned in Bundle.Tag");
		}
	}
}
