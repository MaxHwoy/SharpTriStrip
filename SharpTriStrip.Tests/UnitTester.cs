using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharpTriStrip.Tests
{
	[TestClass]
	public class UnitTester
	{
		[TestMethod]
		public void MemsetTest()
		{
			const byte setter = 0xAA;

			var buffer = new byte[0x1000];
			Utils.Memset(buffer, setter, buffer.Length);

			Assert.IsTrue(InternalMemsetCheck());

			bool InternalMemsetCheck()
			{
				for (int i = 0; i < buffer.Length; ++i)
				{
					if (buffer[i] != setter)
					{
						return false;
					}
				}

				return true;
			}
		}

		[TestMethod]
		public void TriStripTest_1()
		{
			UnitTester.InternalTriStripTest(@"TriStripTests\TriStripTest_1.txt");
		}

		[TestMethod]
		public void TriStripTest_2()
		{
			UnitTester.InternalTriStripTest(@"TriStripTests\TriStripTest_2.txt");
		}

		[TestMethod]
		public void TriStripTest_3()
		{
			UnitTester.InternalTriStripTest(@"TriStripTests\TriStripTest_3.txt");
		}

		[TestMethod]
		public void TriStripTest_4()
		{
			UnitTester.InternalTriStripTest(@"TriStripTests\TriStripTest_4.txt");
		}

		[TestMethod]
		public void TriStripTest_5()
		{
			UnitTester.InternalTriStripTest(@"TriStripTests\TriStripTest_5.txt");
		}

		[TestMethod]
		public void TriStripTest_6()
		{
			UnitTester.InternalTriStripTest(@"TriStripTests\TriStripTest_6.txt");
		}

		[TestMethod]
		public void TriStripTest_7()
		{
			UnitTester.InternalTriStripTest(@"TriStripTests\TriStripTest_7.txt");
		}

		[TestMethod]
		public void TriStripTest_8()
		{
			UnitTester.InternalTriStripTest(@"TriStripTests\TriStripTest_8.txt");
		}

		private static void InternalTriStripTest(string file)
		{
			Assert.IsTrue(File.Exists(file));

			var indices = new List<ushort>();

			using (var sr = new StreamReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
			{
				while (!sr.EndOfStream)
				{
					var line = sr.ReadLine();

					if (String.IsNullOrWhiteSpace(line) || line.StartsWith("//") || line.StartsWith("#"))
					{
						continue;
					}

					var array = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

					for (int i = 0; i < array.Length; ++i)
					{
						indices.Add(Convert.ToUInt16(array[i]));
					}
				}
			}

			var triStrip = new TriStrip();

			triStrip.DisableRestart();
			triStrip.SetCacheSize(16);
			triStrip.SetListsOnly(false);
			triStrip.SetMinStripSize(0);
			triStrip.SetStitchStrips(false);

			Assert.IsTrue(triStrip.GenerateStrips(indices.ToArray(), out var primitives, true));
			Assert.IsNotNull(primitives);
			Assert.IsTrue(primitives.Length > 0);
		}
	}
}
