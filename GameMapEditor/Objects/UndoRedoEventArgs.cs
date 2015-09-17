using System;

namespace GameMapEditor.Objects
{
    public class UndoRedoEventArgs : EventArgs
    {
        private object currentItem;

        public object CurrentItem
        {
            get { return this.currentItem; }
        }

        public UndoRedoEventArgs(object currentItem)
        {
            this.currentItem = currentItem;
        }
    }
}
