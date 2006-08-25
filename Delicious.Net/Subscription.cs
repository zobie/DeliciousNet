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
using System.Collections.Generic;
using System.Xml;

using Delicious.Exceptions;

namespace Delicious
{
	public class Subscription
	{
		private string _User;
		private string _Tag;


		public Subscription ()
		{
		}


		public Subscription (string user, string tag)
		{
			this.User = user;
			this.Tag = tag;
		}


		public string User
		{
			get { return this._User; }
			set { this._User = value; }
		}

		public string Tag
		{
			get { return this._Tag; }
			set { this._Tag = value; }
		}


		public override string ToString ()
		{
			return this.User + " : " + this.Tag;
		}


		public override bool Equals (object obj)
		{
			if (!(obj is Subscription))
				return false;

			return (this == (Subscription)obj);
		}


		public override int GetHashCode ()
		{
			return this.ToString().GetHashCode();
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


		public static bool operator == (Subscription subscription1, Subscription subscription2)
		{
			if ((object)subscription1 == null)
				return ((object)subscription2 == null);
			else if ((object)subscription2 == null)
				return false;

			return (subscription1.User == subscription2.User &&
			        subscription1.Tag == subscription2.Tag);
		}


		public static bool operator != (Subscription subscription1, Subscription subscription2)
		{
			return !(subscription1 == subscription2);
		}


		/// <summary>
		/// Add a subscription
		/// </summary>
		/// <param name="user"></param>
		/// <param name="tag"></param>
		/// <returns><c>true</c> if the subscription is sucessfully added, <c>false</c> otherwise</returns>
		public static bool Add (string user, string tag)
		{
			if (user == null || user.Length == 0)
				throw new DeliciousException ("The 'user' cannot be null or empty string");

			string connectUrl = Constants.RelativeUrl.InboxSub;
			connectUrl = Utilities.AddParameter (connectUrl, Constants.UrlParameter.User, user);
			if (tag != null && tag.Length > 0)
				connectUrl = Utilities.AddParameter (connectUrl, Constants.UrlParameter.Tag, tag);

			XmlDocument xmlDoc = Connection.Connect (connectUrl);
			string resultCode = Utilities.ParseForResultCode (xmlDoc.DocumentElement);
			return (resultCode == Constants.ReturnCode.XmlTagOk);
		}


		/// <summary>
		/// Get a List of <c>Subscription</c> objects
		/// </summary>
		/// <returns>List of <c>Subscription</c> objects</returns>
		public static List<Subscription> Get ()
		{
			string connectUrl = Constants.RelativeUrl.InboxSubs;

			XmlDocument xmlDoc = Connection.Connect (connectUrl);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Sub);
			List<Subscription> subscriptions = new List<Subscription> (nodeList.Count);

			foreach (XmlNode node in nodeList)
			{
				string user = node.Attributes[ Constants.XmlAttribute.User ].Value;
				string tag = node.Attributes[ Constants.XmlAttribute.Tag ].Value;

				Subscription subscription = new Subscription (user, tag);
				subscriptions.Add (subscription);
			}
			return subscriptions;
		}


		/// <summary>
		/// Remove a subscription
		/// </summary>
		/// <param name="user"></param>
		/// <param name="tag"></param>
		/// <returns><c>true</c> if the subscription is sucessfully removed, <c>false</c> otherwise</returns>
		public static bool Remove (string user, string tag)
		{
			if (user == null || user.Length == 0)
				throw new DeliciousException ("The 'user' cannot be null or empty string");

			string connectUrl = Constants.RelativeUrl.InboxUnsub;
			connectUrl = Utilities.AddParameter (connectUrl, Constants.UrlParameter.User, user);
			if (tag != null && tag.Length > 0)
				connectUrl = Utilities.AddParameter (connectUrl, Constants.UrlParameter.Tag, tag);

			XmlDocument xmlDoc = Connection.Connect (connectUrl);
			string resultCode = Utilities.ParseForResultCode (xmlDoc.DocumentElement);
			return (resultCode == Constants.ReturnCode.XmlTagOk);
		}


		#endregion Static Methods
	}
}