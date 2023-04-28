using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Text2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            var pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            var document = (FlowDocument)docViewer.Document;
            var ph = document.PageHeight;
            var pw = document.PageWidth;
            var cw = document.ColumnWidth;
            var cg = document.ColumnGap;
            var pp = document.PagePadding;

            document.PageHeight = pd.PrintableAreaHeight;
            document.PageWidth = pd.PrintableAreaWidth;
            document.ColumnWidth = pd.PrintableAreaWidth;
            document.ColumnGap = 0;
            document.PagePadding = new Thickness(50);
            IDocumentPaginatorSource dps = document;
            pd.PrintDocument(dps.DocumentPaginator, "Text2");

            document.PageHeight = ph;
            document.PageWidth = pw;
            document.ColumnWidth = cw;
            document.ColumnGap = cg;
            document.PagePadding = pp;
        }
    }
}
