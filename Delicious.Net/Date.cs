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

namespace Delicious
{
	public class Date
	{
		private string _Value;
		private int _Count;


		/// <summary>
		/// Construct a new <c>Date</c>
		/// </summary>
		public Date ()
		{
		}


		public Date (string date, int count)
		{
			this.Value = date;
			this.Count = count;
		}


		/// <summary>
		/// A string representing the date in the <c>Date</c> object
		/// </summary>
		public string Value
		{
			get { return this._Value; }
			set { this._Value = value; }
		}

		public int Count
		{
			get { return this._Count; }
			set { this._Count = value; }
		}


		public override string ToString ()
		{
			return this.Value + " : " + this.Count;
		}


		public override bool Equals (object obj)
		{
			if (!(obj is Date))
				return false;

			return (this == (Date)obj);
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


		public static bool operator == (Date date1, Date date2)
		{
			if ((object)date1 == null)
				return ((object)date2 == null);
			else if ((object)date2 == null)
				return false;

			return (date1.Value == date2.Value &&
			        date1.Count == date2.Count);
		}


		public static bool operator != (Date date1, Date date2)
		{
			return !(date1 == date2);
		}


		/// <summary>
		/// Returns a list of <c>Date</c> objects
		/// </summary>
		/// <returns>List of <c>Date</c> objects</returns>
		public static List<Date> GetWithPost ()
		{
			return GetWithPost (null);
		}


		/// <summary>
		/// Returns a list of <c>Date</c> objects
		/// </summary>
		/// <param name="tag">Filter by this tag</param>
		/// <returns>List of <c>Date</c> objects</returns>
		public static List<Date> GetWithPost (string tag)
		{
			string connectUrl = Constants.RelativeUrl.PostsDates;
			if (tag != null && tag.Length > 0)
				connectUrl = Utilities.AddParameter (connectUrl, Constants.UrlParameter.Tag, tag);

			XmlDocument xmlDoc = Connection.Connect (connectUrl);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Date);
			List<Date> dates = new List<Date> (nodeList.Count);

			foreach (XmlNode node in nodeList)
			{
				string date = node.Attributes[ Constants.XmlAttribute.Date ].Value;
				int count = int.Parse (node.Attributes[ Constants.XmlAttribute.Count ].Value);

				Date subscription = new Date (date, count);
				dates.Add (subscription);
			}

			return dates;
		}


		/// <summary>
		/// Returns a list of <c>Date</c> objects containing Inbox entries
		/// </summary>
		/// <returns>List of <c>Date</c> objects containing Inbox entries</returns>
		public static List<Date> GetWithInboxEntries ()
		{
			string connectUrl = Constants.RelativeUrl.InboxDates;

			XmlDocument xmlDoc = Connection.Connect (connectUrl);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Date);
			List<Date> dates = new List<Date> (nodeList.Count);

			foreach (XmlNode node in nodeList)
			{
				string date = node.Attributes[ Constants.XmlAttribute.Date ].Value;
				int count = int.Parse (node.Attributes[ Constants.XmlAttribute.Count ].Value);

				Date subscription = new Date (date, count);
				dates.Add (subscription);
			}

			return dates;
		}


		#endregion Static Methods
	}
}