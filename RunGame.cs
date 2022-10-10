using System;
using Jogo;

namespace Runner
{
    class RunGame
    {
        static string RandomWord()
        {
            string arquivo = @".\palavras.txt";
            string[] palavras = System.IO.File.ReadAllLines(arquivo);
            
            Random rnd = new Random();
            int rndInt = rnd.Next(0,palavras.Length - 1);

            return palavras[rndInt];
        }

        public static void Game()
        {
            Jogo.JogoForca forca = new Jogo.JogoForca(RandomWord());

            while(!forca.Terminou())
            {
                forca.StatusJogo();
                
                Console.Write("Sua tentativa: ");
                string? input = Console.ReadLine();
                char tent;
                int n;

                if(char.TryParse(input, out tent) & !int.TryParse(input, out n))
                {
                    forca.Advinhar(tent);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Input inválido!");
                    continue;
                }
            }

            Console.Clear();
            forca.StatusJogo();

            if(forca.Ganhou())
            {
                Console.WriteLine("Parabéns! Você venceu!");
            }
            else
            {
                Console.WriteLine("Game over! Você Perdeu!");
                forca.MostrarPalavra();
            }
        }
    }
}