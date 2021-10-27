using System.Linq;
using Alba.CsConsoleFormat;

namespace Game
{
    class Help
    {
        private Document doc;
        private readonly LineThickness headerThickness = new LineThickness();

        public Help(string[] moves, Rules rules)

        {
            doc = new Document(
                new Grid
                {
                    Columns = {
                        new Column { Width = GridLength.Auto },

                        moves.Select(item => new[] {
                            new Column { Width = GridLength.Auto }
                        })
                    },
                    Children = {
                        
                        new Cell("User \\ PC") { Stroke = headerThickness },

                        moves.Select(item => new[] {
                            new Cell(item),
                        }),
                    }
                }
            );

            
            for (int row = 0; row < rules.RullesArray.GetLength(0); row++)
            {
                doc.Children[0].Children.Add(new Cell(moves[row]));
                for (int col = 0; col < rules.RullesArray.GetLength(1); col++)
                {
                    Result Result = rules.RullesArray[row, col];
                    doc.Children[0].Children.Add(new Cell(Result){  });
                }
            }
            
        }

        public void GetHelp()
        {
            ConsoleRenderer.RenderDocument(doc);
        }

    }
}
