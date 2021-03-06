﻿// 
// EnumUnderlyingTypeIsIntTests.cs
// 
// Author:
//      Luís Reis <luiscubal@gmail.com>
// 
// Copyright (c) 2013 Luís Reis
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

using ICSharpCode.NRefactory.PlayScript.CodeActions;
using ICSharpCode.NRefactory.PlayScript.Refactoring;
using NUnit.Framework;

namespace ICSharpCode.NRefactory.PlayScript
{
	[TestFixture]
	public class EnumUnderlyingTypeIsIntTests : InspectionActionTestBase
	{
		[Test]
		public void TestCase()
		{
			Test<EnumUnderlyingTypeIsIntIssue>(@"
public enum Foo : int
{
	Bar
}", @"
public enum Foo
{
	Bar
}");
		}

		[Test]
		public void TestDisable()
		{
			TestWrongContext<EnumUnderlyingTypeIsIntIssue>(@"
// ReSharper disable once EnumUnderlyingTypeIsInt
public enum Foo : int
{
    Bar
}");
		}


		[Test]
		public void TestNestedCase()
		{
			Test<EnumUnderlyingTypeIsIntIssue>(@"
class Outer
{
	public enum Foo : int
	{
		Bar
	}
}", @"
class Outer
{
	public enum Foo
	{
		Bar
	}
}");
		}

		[Test]
		public void TestDisabledForNoUnderlyingType()
		{
			Test<EnumUnderlyingTypeIsIntIssue>(@"
public enum Foo
{
	Bar
}", 0);
		}

		[Test]
		public void TestDisabledForOtherTypes()
		{
			Test<EnumUnderlyingTypeIsIntIssue>(@"
public enum Foo : byte
{
	Bar
}", 0);
		}
	}
}