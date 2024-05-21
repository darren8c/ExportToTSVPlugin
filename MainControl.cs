using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Paratext.PluginInterfaces;

namespace ExportToTSV
{
    public partial class MainControl : EmbeddedPluginControl
    {
        private IVerseRef m_Reference;
        private IProject m_project;
        private List<IUSFMToken> m_Tokens;
        private IWriteLock m_WriteLock;

        public MainControl()
        {
            InitializeComponent();
            m_WriteLock = null;
        }

        public override void OnAddedToParent(IPluginChildWindow parent, IWindowPluginHost host, string state)
        {
            parent.SetTitle(ExportToTSVControl.pluginName);

            SetProject(parent.CurrentState.Project);
            m_Reference = parent.CurrentState.VerseRef;
            m_WriteLock = null;

            parent.WindowClosing += WindowClosing;
            parent.ProjectChanged += ProjectChanged;
        }

        public override string GetState()
        {
            return null;
        }

        public override void DoLoad(IProgressInfo progressInfo)
        {
        }

        private void Unlock()
        {
            if (m_WriteLock != null)
            {
                IWriteLock temp = m_WriteLock;
                temp.Dispose();
                m_WriteLock = null;
            }
        }

        private void ReleaseRequested(IWriteLock writeLock)
        {
            Unlock();
        }

        private void WindowClosing(IPluginChildWindow sender, CancelEventArgs args)
        {
            Unlock();
        }

        private void ProjectChanged(IPluginChildWindow sender, IProject newProject)
        {
            // Save the old project first:
            Unlock();

            // Then remember the new project
            SetProject(newProject);
        }

        private void ScriptureDataChangedHandler(IProject sender, int bookNum, int chapterNum)
        {
            Unlock();
        }

        private void SetProject(IProject newProject)
        {
            if (m_project != null)
            {
                m_project.ScriptureDataChanged -= ScriptureDataChangedHandler;
            }

            m_project = newProject;
            if (newProject != null)
            {
                newProject.ScriptureDataChanged += ScriptureDataChangedHandler;
            }
        }

        public void ExportScripture(object sender, EventArgs e)
        {
            if (m_project == null)
            {
                MessageBox.Show("No project selected");
                return;
            }

            if (m_WriteLock != null)
            {
                MessageBox.Show("'Quit' to release current lock before getting more Scripture");
                return;
            }

            //Get all of the books for the current project
            m_WriteLock = m_project.RequestWriteLock(this, ReleaseRequested, m_Reference.BookNum, /*m_Reference.ChapterNum*/ 0);
            var books = m_project.AvailableBooks;

            progressBar.Visible = true;
            progressBar.CustomText = $"Exporting";
            progressBar.Maximum = books.Count;
            progressBar.ResetText();
            progressBar.Value = 0;

            //loop through all of the books and get the usfm tokens
            foreach (var book in books)
            {
                StringBuilder sb = new StringBuilder();
                var tokens = m_project.GetUSFMTokens(book.Number, 0);
                var lastVerseRef = m_Reference;
                string verseText = "";

                //loop through the tokens.  If we find a marker token we always ignore unless it is a verse marker.
                //The verse marker is how we determine we should write out the verse because a new one is starting.
                //If it is not a marker, and it is scripture it is verse text.
                foreach (var token in tokens)
                {
                    if (token is IUSFMMarkerToken marker)
                    {
                        switch (marker.Type)
                        {
                            case MarkerType.Verse:
                                if (!String.IsNullOrWhiteSpace(verseText)) 
                                {
                                    sb.AppendLine(lastVerseRef.BookCode + "\t" + lastVerseRef.ChapterNum + "\t" + lastVerseRef.VerseNum + "\t" + verseText);
                                    verseText = "";
                                }
                                lastVerseRef = marker.VerseRef;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (token.IsScripture && !string.IsNullOrEmpty(token.ToString()))
                        verseText += token.ToString();
                }
                
                var saveDataId = $"{book.Code}.tsv";
                var writeLock = m_project.RequestWriteLock(this, ReleaseRequested, saveDataId);
                m_project.PutPluginData(writeLock, this, saveDataId, writer => writer.Write(sb.ToString()));
                progressBar.Increment(1);
                //progressBar.CustomText = $"Exporting {progressBar.Value} of {books.Count}";
            }

            progressBar.CustomText = $"Complete";

            if (m_WriteLock == null)
            {
                Unlock();
                MessageBox.Show("Unable to get a Write Lock");
            }
        }
    }

    class TextToken : IUSFMTextToken
    {
        public TextToken(IUSFMTextToken token)
        {
            Text = token.Text;
            VerseRef = token.VerseRef;
            VerseOffset = token.VerseOffset;
            IsSpecial = token.IsSpecial;
            IsFigure = token.IsFigure;
            IsFootnoteOrCrossReference = token.IsFootnoteOrCrossReference;
            IsScripture = token.IsScripture;
            IsMetadata = token.IsMetadata;
            IsPublishableVernacular = token.IsPublishableVernacular;
        }

        public string Text { get; set; }

        public IVerseRef VerseRef { get; set; }

        public int VerseOffset { get; set; }

        public bool IsSpecial { get; set; }

        public bool IsFigure { get; set; }

        public bool IsFootnoteOrCrossReference { get; set; }

        public bool IsScripture { get; set; }

        public bool IsMetadata { get; set; }

        public bool IsPublishableVernacular { get; set; }
    }
}
