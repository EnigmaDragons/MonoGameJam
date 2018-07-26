using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface.Layouts
{
    public class GridLayout : IVisual
    {
        private readonly Layout _layout = new Layout();
        private readonly List<Definition> _columns;
        private readonly List<Definition> _rows;

        public Size2 Size { get; set; }

        public GridLayout(Size2 size, int columns, int rows) : this(size, 
            Enumerable.Repeat(new ShareDefintion(), columns).Cast<Definition>().ToList(),
            Enumerable.Repeat(new ShareDefintion(), rows).Cast<Definition>().ToList()) { }

        public GridLayout(Size2 size, List<Definition> columns, int rows) 
            : this(size, columns, Enumerable.Repeat(new ShareDefintion(), rows).Cast<Definition>().ToList()) { }

        public GridLayout(Size2 size, int columns, List<Definition> rows)
            : this(size, Enumerable.Repeat(new ShareDefintion(), columns).Cast<Definition>().ToList(), rows) { }

        public GridLayout(Size2 size, List<Definition> columns, List<Definition> rows)
        {
            Size = size;
            _columns = columns;
            _rows = rows;

            if (_columns.Any(x => x.Size == SizeType.Share))
            {
                decimal widthRemaining = Size.Width - _columns.Where(x => x.Size != SizeType.Share).Sum(x => x.Length);
                decimal shareWidth = widthRemaining / _columns.Where(x => x.Size == SizeType.Share).Sum(x => x.Shares);
                _columns.Where(x => x.Size == SizeType.Share).ForEach(x => x.Length = x.Shares * shareWidth);
            }

            if (_rows.Any(x => x.Size == SizeType.Share))
            {
                decimal heightRemaining = Size.Height - _rows.Where(x => x.Size != SizeType.Share).Sum(x => x.Length);
                decimal shareHeight = heightRemaining / _rows.Where(x => x.Size == SizeType.Share).Sum(x => x.Shares);
                _rows.Where(x => x.Size == SizeType.Share).ForEach(x => x.Length = x.Shares * shareHeight);
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            _layout.Draw(parentTransform);
        }

        public void Add(IVisual visual, int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            Add(visual, new Transform2(GetBlockSize(column, row, columnSpan, rowSpan)), column, row, columnSpan, rowSpan);
        }

        public void Add(IVisual visual, Transform2 objTransform, int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            var xOffset = GetOffset(_columns, column, columnSpan, objTransform.Size.Width);
            var yOffset = GetOffset(_rows, row, rowSpan, objTransform.Size.Height);
            _layout.Add(visual, new Transform2(new Vector2((int)xOffset, (int)yOffset), objTransform.Size));
        }

        public void AddSpatial(IVisual visual, Transform2 objTransform, int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            var xOffset = GetOffset(_columns, column, columnSpan, objTransform.Size.Width);
            var yOffset = GetOffset(_rows, row, rowSpan, objTransform.Size.Height);
            _layout.Add(visual, new Vector2((int)xOffset, (int)yOffset));
        }

        public void Remove(IVisual visual)
        {
            _layout.Remove(visual);
        }

        public void Clear()
        {
            _layout.Clear();
        }

        public Size2 GetBlockSize(int column, int row)
        {
            return GetBlockSize(column, row, 1, 1);
        }

        public Size2 GetBlockSize(int column, int row, int columnSpan, int rowSpan)
        {
            return new Size2(
                (int) _columns.Skip(column - 1).Take(columnSpan).Sum(x => x.Length),
                (int) _rows.Skip(row - 1).Take(rowSpan).Sum(x => x.Length));
        }

        private decimal GetOffset(List<Definition> definitions, int definitionStart, int definitionSpan, int objLength)
        {
            var definitionOffset = definitions.Take(definitionStart - 1).Sum(x => x.Length);
            var definitionsLength = definitions.Skip(definitionStart - 1).Take(definitionSpan).Sum(x => x.Length);
            var offset = definitionsLength / 2 - (decimal)objLength / 2;
            return definitionOffset + offset;
        }
    }
}
