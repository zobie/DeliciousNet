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
	public class DateTests : TestBase
	{
		[Test]
		public void ObjectEquality ()
		{
			Date d1 = new Date();
			d1.Value = System.DateTime.Now.ToUniversalTime().ToString();
			d1.Count = 42;

			Date d2 = new Date (d1.Value, d1.Count);

			Assert.IsTrue (d1 == d2, "(d1 == d2) should be true");
			Assert.IsFalse (d1 != d2, "(d1 != d2) should be false");
			Assert.IsTrue (d1.Equals (d2), "d1.Equals(d2) should be true");

			Assert.IsTrue (d1.ReferenceEquals (d1), "d1.ReferenceEquals(d1) should be true");
			Assert.IsFalse (d1.ReferenceEquals (d2), "d1.ReferenceEquals(d2) should be false");
		}


		[Test]
		[Ignore ("TODO: Not yet implemented")]
		public void GetWithPost ()
		{
		}


		[Test]
		[Ignore ("TODO: Not yet implemented")]
		public void GetWithInboxEntries ()
		{
		}
	}
}
