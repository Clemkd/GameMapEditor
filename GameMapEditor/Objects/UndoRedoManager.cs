using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects
{
    // TODO : Corriger 
    public class UndoRedoManager
    {
        // TODO : Implémenter la limite
        private const int MAX_LENGHT = 6;

        private GameMap currentItem;
        private Stack<GameMap> undoList;
        private Stack<GameMap> redoList;

        public event EventHandler<UndoRedoEventArgs> UndoHappened;
        public event EventHandler<UndoRedoEventArgs> RedoHappened;

        public UndoRedoManager()
        {
            this.undoList = new Stack<GameMap>();
            this.redoList = new Stack<GameMap>();
        }

        public void Add(GameMap map)
        {
            if (this.currentItem != null)
            {
                this.undoList.Push(map);
            }

            this.currentItem = map;
            this.redoList.Clear();
        }

        public void Undo()
        {
            if (!this.CanUndo)
            {
                throw new IndexOutOfRangeException("Impossible d'effectuer l'opération d'annulation");
            }

            this.redoList.Push(this.currentItem);
            this.currentItem = this.undoList.Pop();
            this.UndoHappened?.Invoke(this, new UndoRedoEventArgs(this.currentItem));
        }

        public void Redo()
        {
            if (!this.CanRedo)
            {
                throw new IndexOutOfRangeException("Impossible d'effectuer l'opération de refonte");
            }

            this.undoList.Push(this.currentItem);
            this.currentItem = this.redoList.Pop();
            this.RedoHappened?.Invoke(this, new UndoRedoEventArgs(this.currentItem));
        }

        public bool CanUndo
        {
            get { return this.undoList.Count > 0; }
        }

        public bool CanRedo
        {
            get { return this.redoList.Count > 0; }
        }
    }
}
