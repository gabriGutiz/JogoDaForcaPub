using System;
using System.Linq;

namespace Jogo
{
    class JogoForca
    {
        private string[] figuras = {
            "   ___________\n  |           |\n  |\n  |\n  |\n  |\n  |\n==================",
            "   ___________\n  |           |\n  |           O\n  |\n  |\n  |\n  |\n==================",
            "   ___________\n  |           |\n  |           O\n  |           |\n  |\n  |\n  |\n==================",
            "   ___________\n  |           |\n  |           O\n  |          /|\n  |\n  |\n  |\n==================",
            "   ___________\n  |           |\n  |           O\n  |          /|\\\n  |\n  |\n  |\n==================",
            "   ___________\n  |           |\n  |           O\n  |          /|\\\n  |          /\n  |\n  |\n==================",
            "   ___________\n  |           |\n  |           O\n  |          /|\\\n  |          / \\\n  |\n  |\n==================",
        };

        private string palavra;
        private string[] palavraArrHide;
        private int tentativas = 0;
        private bool ativo;
        private string letrasTentativas;
        private string erros;

        public JogoForca(string palavra)
        {
            this.palavra = palavra;
            this.ativo = true;
            char[] palavraArr = this.palavra.ToCharArray();
            this.letrasTentativas = "";
            this.erros = "";

            this.palavraArrHide = new string[palavraArr.Length];
            
            for(int i=0;i<palavraArr.Length;i++)
            {
                this.palavraArrHide[i] = "_";
            }
        }

        public void Advinhar(char tentativa)
        {
            Console.Clear();

            if(this.letrasTentativas.Split(tentativa).Length - 1 == 0)
            {
                this.letrasTentativas += Convert.ToString(tentativa);
                tentativa = Char.ToLower(tentativa);
                int[] indexes = this.IndexStr(this.palavra, tentativa);

                if(indexes[0] == -1)
                {
                    Console.WriteLine("Errou!");
                    if(this.tentativas < this.figuras.Length)
                    {
                        this.tentativas += 1;
                    }
                    this.erros += Convert.ToString(tentativa);
                }
                else
                {
                    foreach(int i in indexes)
                    {
                        if(i==0)
                        {
                            this.palavraArrHide[i] = (Convert.ToString(tentativa)).ToUpper();
                        }
                        else
                        {
                            this.palavraArrHide[i] = Convert.ToString(tentativa);
                        }
                    }
                    Console.WriteLine("Acertou!");
                }
            }
            else
            {
                Console.WriteLine("Você já tentou esta letra!");
            }
        }

        public bool Terminou()
        {
            this.ativo = !(this.tentativas == this.figuras.Length | this.palavra == string.Join("",this.palavraArrHide).ToLower());
            return !this.ativo;
        }

        public bool Ganhou()
        {
            return (this.palavra == string.Join("", this.palavraArrHide).ToLower());
        }

        public void StatusJogo()
        {
            Console.WriteLine("Seus erros: [ {0} ]",string.Join(", ", erros.ToCharArray()));
            if(this.Terminou())
            {
                if(this.Ganhou())
                {
                    Console.WriteLine(this.figuras[this.tentativas]);
                }
                else
                {
                    Console.WriteLine(this.figuras[this.tentativas-1]);
                }
            }
            else
            {
                Console.WriteLine(this.figuras[this.tentativas]);
            }
            Console.WriteLine(this.EsconderPalavra(this.palavraArrHide)+"\n");
        }

        public void MostrarPalavra()
        {
            Console.WriteLine("A palavra era: {0}",(char.ToUpper(string.Join(" ", this.palavra)[0]) + string.Join(" ", this.palavra).Substring(1)));
        }

        private string EsconderPalavra(string[] letras)
        {
            return (char.ToUpper(string.Join(" ", letras)[0]) + string.Join(" ", letras).Substring(1));
        }

        private int[] IndexStr(string mainStr, char searchChar)
        {
            int[] indexes;
            int index = mainStr.IndexOf(searchChar, 0);
            int freq = mainStr.Split(searchChar).Length - 1;

            if(freq == 0)
            {
                indexes = new int[] { -1 };
            }
            else
            {
                indexes = new int[freq];
                int i = 0;
                while(index != -1)
                {
                    indexes[i] = index;
                    index = mainStr.IndexOf(searchChar, index+1);
                    i++;
                }
            }
            return indexes;
        }
    }
}