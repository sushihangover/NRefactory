﻿// 
// ReplaceWithSingleCallToFirstIssueTests.cs
//
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2013 Xamarin <http://xamarin.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using NUnit.Framework;
using ICSharpCode.NRefactory.PlayScript.Refactoring;
using ICSharpCode.NRefactory.PlayScript.CodeActions;

namespace ICSharpCode.NRefactory.PlayScript
{
	[TestFixture]
	public class ReplaceWithSingleCallToFirstIssueTests : InspectionActionTestBase
	{
		[Test]
		public void TestSimpleCase()
		{
			var input = @"using System.Linq;
public class CSharpDemo {
	public void Bla () {
		int[] arr;
		var bla = arr.Where (x => x < 4).First ();
	}
}";

			TestRefactoringContext context;
			var issues = GetIssues(new ReplaceWithSingleCallToFirstIssue(), input, out context);
			Assert.AreEqual(1, issues.Count);
			CheckFix(context, issues, @"using System.Linq;
public class CSharpDemo {
	public void Bla () {
		int[] arr;
		var bla = arr.First (x => x < 4);
	}
}");
		}

		[Test]
		public void TestDisable()
		{
			var input = @"using System.Linq;
public class CSharpDemo {
	public void Bla () {
		int[] arr;
// ReSharper disable ReplaceWithSingleCallToFirst
		var bla = arr.Where (x => x < 4).First ();
	}
}";

			TestRefactoringContext context;
			var issues = GetIssues(new ReplaceWithSingleCallToFirstIssue(), input, out context);
			Assert.AreEqual(0, issues.Count);
		}
	}
}
