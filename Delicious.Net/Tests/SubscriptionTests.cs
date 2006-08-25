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
 *		notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above
 *		copyright notice, this list of conditions and the following
 *		disclaimer in the documentation and/or other materials provided with
 *		the distribution.
 * Neither the name of "Nate Zobrist" nor the names of its
 *		contributors may be used to endorse or promote products derived from
 *		this software without specific prior written permission.
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

using Delicious.Exceptions;

using NUnit.Framework;

namespace Delicious.Tests
{
	[TestFixture]
	public class SubscriptionTests : TestBase
	{
		[Test]
		public void ObjectEquality ()
		{
			Subscription s1 = new Subscription();
			s1.User = this.GetRandomString();
			s1.Tag = this.GetRandomString();

			Subscription s2 = new Subscription (s1.User, s1.Tag);

			Assert.IsTrue (s1 == s2, "(s1 == s2) should be true");
			Assert.IsFalse (s1 != s2, "(s1 != s2) should be false");
			Assert.IsTrue (s1.Equals (s2), "s1.Equals(s2) should be true");

			Assert.IsTrue (s1.ReferenceEquals (s1), "s1.ReferenceEquals(s1) should be true");
			Assert.IsFalse (s1.ReferenceEquals (s2), "s1.ReferenceEquals(s2) should be false");
		}


		[Test]
		[Ignore ("TODO: Not yet implemented")]
		public void Add ()
		{
		}


		[Test]
		[Ignore ("TODO: Not yet implemented")]
		public void Get ()
		{
		}


		[Test]
		[Ignore ("TODO: Not yet implemented")]
		public void Remove ()
		{
		}
	}
}
