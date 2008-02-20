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

using Delicious.Exceptions;

using NUnit.Framework;

#if DEBUG
namespace Delicious.Tests
{
	[TestFixture]
	public class ConnectionTests : TestBase
	{
		[Test]
		[Ignore ("TODO: not yet implemented")]
		public void LastUpdateTime ()
		{
		}

		[Test]
		public void ConnectToDelicious ()
		{
			DateTime lastUpdated = Connection.LastUpdated ();
			Assert.AreNotEqual (lastUpdated, DateTime.MinValue, "Delicious.LastUpdate() returned an invalid value");
		}

		[Test]
		public void NotAuthorizedException ()
		{
			bool exceptionThrown = false;
			string username = Connection.Username;
			string password = Connection.Password;

			Connection.Username = "xyzzy";
			Connection.Password = "plugh";

			try
			{
				Connection.LastUpdated ();
			}
			catch (DeliciousNotAuthorizedException)
			{
				exceptionThrown = true;
			}
			finally
			{
				Connection.Username = username;
				Connection.Password = password;
			}

			if (!exceptionThrown)
				Assert.Fail ("An invalid username/password combination should have thrown a DeliciousNotAuthorizedException.");
		}
	}
}
#endif