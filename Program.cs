using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                bool flag = true;
                string pathSource = string.Empty;
                string newPathSource = string.Empty;
                string name = string.Empty;

                RedBlackTree RBTree = new RedBlackTree();

                while (flag)
                {
                    //Console.Clear();
                    // Start menu:
                    Console.WriteLine("WELCOME TO RED_BLACK_TREE PROGRAMM");
                    Console.WriteLine("Menu:");
                    Console.WriteLine("'lex'  -  load example file");
                    Console.WriteLine("'delw' -  удалить слово из дерева");
                    Console.WriteLine("'addw' -  добавить слово в дерево");
                    Console.WriteLine("'addf' -  добавить слова в дерево из файла");
                    Console.WriteLine("'search' -  найти слово");
                    Console.WriteLine("'clear' - очистить дерево");
                    Console.WriteLine("'exit' - for exit");

                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "lex":
                        case "LEX":
                            pathSource = @"E:\Polytech\TGraph\Самобалансирующиеся деревья\__FILES\HellGirl.txt";

                            using (StreamReader sr = new StreamReader(pathSource, Encoding.Default))
                            {
                                try
                                {
                                    List<string> Words = sr.Words();

                                    RBTree = new RedBlackTree();

                                    foreach (string w in Words)
                                        RBTree.AddData(w);

                                    if (RBTree.Count < 50)
                                        RBTree.PrintTreeConsoleVertical();
                                    else
                                        RBTree.PrintTreeConsoleHorizontal();

                                    Console.WriteLine();
                                }
                                catch (Exception el)
                                {
                                    if ((el as FormatException) != null)
                                        Console.WriteLine("Incorrect format in file");
                                    else
                                        Console.WriteLine(el.Message);
                                }
                            }
                            Console.WriteLine("Press any key for continue....");
                            Console.ReadLine();
                            break;
                        case "delw":
                        case "DELW":
                            try
                            {
                                Console.WriteLine("Deletion. Input word: ");

                                string oldWord = Console.ReadLine();
                                oldWord = oldWord.ToLowerInvariant();

                                if (!RBTree.SearchWord(oldWord))
                                    Console.WriteLine("Такого слова нет в словаре!");
                                else
                                    RBTree.DeleteData(oldWord);

                                if (RBTree.Count < 50)
                                    RBTree.PrintTreeConsoleVertical();
                                else
                                    RBTree.PrintTreeConsoleHorizontal();

                                Console.WriteLine();

                            }
                            catch (Exception el)
                            {
                                Console.WriteLine(el.Message);
                            }      
                            Console.WriteLine("Press any key for continue....");
                            Console.ReadLine();
                            break;
                        case "addw":
                        case "ADDW":
                            try
                            {
                                Console.WriteLine("Добавление слова: ");

                                string newWord = Console.ReadLine();
                                newWord = newWord.ToLowerInvariant();

                                if (RBTree.SearchWord(newWord))
                                    Console.WriteLine("Слово уже есть в словаре!");
                                else
                                    RBTree.AddData(newWord);

                                if (RBTree.Count < 50)
                                    RBTree.PrintTreeConsoleVertical();
                                else
                                    RBTree.PrintTreeConsoleHorizontal();

                                Console.WriteLine();

                            }
                            catch (Exception el)
                            {
                                Console.WriteLine(el.Message);
                            }
                            Console.WriteLine("Press any key for continue....");
                            Console.ReadLine();
                            break;
                        case "addf":
                        case "ADDF":
                            Console.WriteLine("Введите имя файла (из папки __FILES):");
                            string filename = Console.ReadLine();
                            newPathSource = @"E:\Polytech\TGraph\Самобалансирующиеся деревья\__FILES\" + filename;

                            using (StreamReader sr = new StreamReader(newPathSource, Encoding.Default))
                            {
                                try
                                {
                                    List<string> Words = sr.Words();

                                    //RBTree = new RedBlackTree();

                                    foreach (string w in Words)
                                        RBTree.AddData(w);

                                    if (RBTree.Count < 50)
                                        RBTree.PrintTreeConsoleVertical();
                                    else
                                        RBTree.PrintTreeConsoleHorizontal();

                                    Console.WriteLine();
                                }
                                catch (Exception el)
                                {
                                    if ((el as FormatException) != null)
                                        Console.WriteLine("Incorrect format in file");
                                    else
                                        Console.WriteLine(el.Message);
                                }
                            }
                            Console.WriteLine("Press any key for continue....");
                            Console.ReadLine();
                            break;
                        case "search":
                        case "SEARCH":
                            try
                            {
                                Console.WriteLine("Введите искомое слово: ");

                                string searchWord = Console.ReadLine();
                                searchWord = searchWord.ToLowerInvariant();
                                

                                if (RBTree.SearchWord(searchWord))
                                    Console.WriteLine("Слово найдено в словаре!");
                                else
                                    Console.WriteLine("Слово НЕ найдено в словаре!");

                                if (RBTree.Count < 50)
                                    RBTree.PrintTreeConsoleVertical();
                                else
                                    RBTree.PrintTreeConsoleHorizontal();

                                Console.WriteLine();

                            }
                            catch (Exception el)
                            {
                                Console.WriteLine(el.Message);
                            }
                            Console.WriteLine("Press any key for continue....");
                            Console.ReadLine();
                            break;
                        case "clear":
                        case "CLEAR":
                            try
                            {
                                Console.WriteLine("Очистка словаря.");
                                RBTree.Clear();
                                Console.WriteLine("Дерево пусто!");
                                Console.WriteLine();
                            }
                            catch (Exception el)
                            {
                                Console.WriteLine(el.Message);
                            }
                            Console.WriteLine("Press any key for continue....");
                            Console.ReadLine();
                            break;
                        case "exit":
                        case "EXIT":
                            flag = false;
                            break;
                        default:
                            break;
                    }
                }  // End of 'while'
            }
            catch (Exception e) // ловим необработанные в ходе программы исключения
            {
                Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
    }
}
