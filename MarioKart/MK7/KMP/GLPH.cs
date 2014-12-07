﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEveryFileExplorer.Collections;
using System.Windows.Forms;
using System.IO;
using LibEveryFileExplorer.GameData;
using LibEveryFileExplorer.Files;
using LibEveryFileExplorer;

namespace MarioKart.MK7.KMP
{
	public class GLPH : GameDataSection<GLPH.GLPHEntry>
	{
		public GLPH() { Signature = "HPLG"; }
		public GLPH(EndianBinaryReader er)
		{
			Signature = er.ReadString(Encoding.ASCII, 4);
			if (Signature != "HPLG") throw new SignatureNotCorrectException(Signature, "HPLG", er.BaseStream.Position - 4);
			NrEntries = er.ReadUInt32();
			for (int i = 0; i < NrEntries; i++) Entries.Add(new GLPHEntry(er));
		}

		public override String[] GetColumnNames()
		{
			return new String[] {
					"ID",
					"Start",
					"Length",
					"Previous 1","Previous 2","Previous 3","Previous 4","Previous 5","Previous 6",
					"Next 1","Next 2","Next 3","Next 4","Next 5","Next 6",
					"?",
					"?"
				};
		}
		public class GLPHEntry : GameDataSectionEntry
		{
			public GLPHEntry()
			{
				Previous = new sbyte[] { -1, -1, -1, -1, -1, -1, -1, -1 };
				Next = new sbyte[] { -1, -1, -1, -1, -1, -1, -1, -1 };
			}
			public GLPHEntry(EndianBinaryReader er)
			{
				Start = er.ReadByte();
				Length = er.ReadByte();
				Previous = er.ReadSBytes(6);
				Next = er.ReadSBytes(6);
				Unknown1 = er.ReadUInt32();
				Unknown2 = er.ReadUInt32();
			}

			public override ListViewItem GetListViewItem()
			{
				ListViewItem m = new ListViewItem("");
				m.SubItems.Add(Start.ToString());
				m.SubItems.Add(Length.ToString());
				m.SubItems.Add(Previous[0].ToString());
				m.SubItems.Add(Previous[1].ToString());
				m.SubItems.Add(Previous[2].ToString());
				m.SubItems.Add(Previous[3].ToString());
				m.SubItems.Add(Previous[4].ToString());
				m.SubItems.Add(Previous[5].ToString());
				m.SubItems.Add(Next[0].ToString());
				m.SubItems.Add(Next[1].ToString());
				m.SubItems.Add(Next[2].ToString());
				m.SubItems.Add(Next[3].ToString());
				m.SubItems.Add(Next[4].ToString());
				m.SubItems.Add(Next[5].ToString());

				m.SubItems.Add(HexUtil.GetHexReverse(Unknown1));
				m.SubItems.Add(HexUtil.GetHexReverse(Unknown2));
				return m;
			}
			public Byte Start { get; set; }
			public Byte Length { get; set; }
			public SByte[] Previous { get; set; }//6
			public SByte[] Next { get; set; }//6
			public UInt32 Unknown1 { get; set; }
			public UInt32 Unknown2 { get; set; }
		}
	}
}