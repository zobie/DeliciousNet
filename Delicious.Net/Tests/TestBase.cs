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
using System.Text;

using Delicious.Exceptions;

using NUnit.Framework;

#if DEBUG
namespace Delicious.Tests
{
	public class TestBase
    {
        protected static List<string> CleanupPostList = new List<string> ();
        protected static List<string> CleanupBundleList = new List<string> ();

		[TestFixtureSetUp]
		public virtual void Init ()
		{
			// TODO: before these tests can be run you must supply a valid username/password combination
			Connection.Username = @"";
			Connection.Password = @"";
		}


		[TestFixtureTearDown]
		public virtual void Dispose ()
		{
            while (CleanupPostList.Count > 0)
            {
                string url = CleanupPostList[ 0 ];
                Post.Delete (url);
                CleanupPostList.Remove (url);
            }
            while (CleanupBundleList.Count > 0)
            {
                string bundle = CleanupBundleList[ 0 ];
                Bundle.Delete (bundle);
                CleanupBundleList.Remove (bundle);
            }
		}


		private static readonly System.Random rand = new System.Random ();
		protected static string GetRandomString ()
		{
		    int minLength = 3;
		    int maxLength = 15;

		    StringBuilder sb = new StringBuilder();
		    int length = minLength + rand.Next (maxLength - minLength + 1);
		    for (int i = 0; i < length; i++)
		    {
		        switch (rand.Next (3))
		        {
		            case 1:
		                sb.Append ((char)('A' + rand.Next (26)));
		                break;
		            case 2:
		                sb.Append ((char)('a' + rand.Next (26)));
		                break;
		            case 0:
		                sb.Append ((char)('0' + rand.Next (10)));
		                break;
		        }
		    }
		    return sb.ToString();
		}

		protected static string GetRandomUrl ()
		{
			return "http://www." + GetRandomString() + ".com/";
		}
	}
}
#endif