using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Paratext.PluginInterfaces;

namespace ExportToTSV
{
    /// <summary>
    /// This plugin will save the project data for all available books in a tabbed delimited format
    /// BOOK_CODE -> CHAPTER_NUM -> VERSE_NUM -> VERSE_TEXT
    /// 
    /// The plugin can be located in the project menu under the advanced tab.  Clicking 'Export Project'
    /// will export the project to {Project}\ExportToTSV\ExportToTSV\{BOOK_CODE}.tsv
    /// 
    /// The main work is done in MainControl.cs -> ExportScripture
    /// </summary>
    public class ExportToTSVControl : IParatextWindowPlugin
	{
		public const string pluginName = "Export To TSV";
		public string Name => pluginName;
		public string GetDescription(string locale) => "Exports USFM via tokens to a tabbed delimitted format.";
		public Version Version => new Version(1, 0);
		public string VersionString => Version.ToString();
		public string Publisher => "SIL/UBS";

		public IEnumerable<WindowPluginMenuEntry> PluginMenuEntries
		{
			get
			{
				yield return new WindowPluginMenuEntry("Export To TSV...", Run, PluginMenuLocation.ScrTextProjectAdvanced);
			}
		}

		public IDataFileMerger GetMerger(IPluginHost host, string dataIdentifier)
		{
            return new MyMerger();
        }

		/// <summary>
		/// Called by Paratext when the menu item created for this plugin was clicked.
		/// </summary>
		private void Run(IWindowPluginHost host, IParatextChildState windowState)
		{
			host.ShowEmbeddedUi(new MainControl(), windowState.Project);
		}
	}

    public class MyMerger : IDataFileMerger
    {
        public string Merge(string theirs, string mine, string parent)
        {
            return mine;
        }
    }
}

