// Fig. 22.30: JoiningTableData.cs
// Using LINQ to perform a join and aggregate data across tables.
using System;
using System.Linq;
using System.Windows.Forms;

namespace JoinQueries
{
   public partial class JoiningTableData : Form
   {
      public JoiningTableData()
      {
         InitializeComponent();
      }

        private void JoiningTableData_Load(object sender, EventArgs e)
        {
            // Entity Framework DbContext
            var dbcontext = new BooksExamples.BooksEntities();

            var titlesandAuthors =
                   from book in dbcontext.Titles
                   from author in book.Authors
                   orderby book.Title1
                   select new { book.Title1, author.FirstName, author.LastName };

            outputTextBox.AppendText("Titles and Authors:");

            foreach (var title in titlesandAuthors)
            {
                outputTextBox.AppendText($"\r\n\t{title.Title1,-60} {title.FirstName,-10} {title.LastName,-10}");
            }

            var titlesandAuthorsSorted =
                      from book in dbcontext.Titles
                      from author in book.Authors
                      orderby book.Title1, author.LastName, author.FirstName
                      select new { book.Title1, author.FirstName, author.LastName };

            outputTextBox.AppendText("\r\n\r\nTitles and Authors, Sorted by Name:");

            foreach (var title in titlesandAuthorsSorted)
            {
                outputTextBox.AppendText($"\r\n\t{title.Title1,-60} {title.FirstName,-10} {title.LastName,-10}");
            }

            var authorsByTitle =
                from book in dbcontext.Titles
                orderby book.Title1
                select new
                {
                    Title = book.Title1,
                    Authors =
                        from author in book.Authors
                        orderby author.LastName, author.FirstName
                        select author,
                };

            outputTextBox.AppendText("\r\n\r\nAuthors grouped by Title:");

            // display titles written by each author, grouped by author
            foreach (var title in authorsByTitle)
            {
                // display author's name
                outputTextBox.AppendText($"\r\n\t{title.Title}:");

                // display titles written by that author
                foreach (var author in title.Authors)
                {
                    outputTextBox.AppendText($"\r\n\t\t{author.LastName, -10} {author.FirstName, -10}");
                }
            }
        }

   }
} 


/**************************************************************************
 * (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
 * Pearson Education, Inc. All Rights Reserved.                           *
 *                                                                        *
 * DISCLAIMER: The authors and publisher of this book have used their     *
 * best efforts in preparing the book. These efforts include the          *
 * development, research, and testing of the theories and programs        *
 * to determine their effectiveness. The authors and publisher make       *
 * no warranty of any kind, expressed or implied, with regard to these    *
 * programs or to the documentation contained in these books. The authors *
 * and publisher shall not be liable in any event for incidental or       *
 * consequential damages in connection with, or arising out of, the       *
 * furnishing, performance, or use of these programs.                     *
 **************************************************************************/
