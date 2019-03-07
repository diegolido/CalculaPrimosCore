using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CalculaPrimosCore
{
    class Program
    {
        static void Main(string[] args)
        {
            long _ultimo = 200000;
            Console.WriteLine("Diferenças Formas de Escrever os Threads para Calcular Números Primos!");
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadLine();
            var programa = new Program();
            programa.ZeraTotalPrimos();
            programa.CronometroParallel(_ultimo);
            Console.WriteLine("Total de primos encontrados: " + programa.RetornaTotalPrimos() + 2);
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadLine();
            programa.ZeraTotalPrimos();
            programa.CronometroNormal(_ultimo);
            Console.WriteLine("Total de primos encontrados: " + programa.RetornaTotalPrimos() + 2);
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadLine();
            programa.ZeraTotalPrimos();
            programa.CronometroTask(_ultimo);
            Console.WriteLine("Total de primos encontrados: " + programa.RetornaTotalPrimos() + 2);
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadLine();
            programa.ZeraTotalPrimos();
            Console.WriteLine("Parallel mais uma vez, só pra garantir rs");
            programa.CronometroParallel(_ultimo);
            Console.WriteLine("Total de primos encontrados: " + programa.RetornaTotalPrimos() + 2);
            Console.WriteLine("Pressione enter para sair...");
            Console.ReadLine();
        }

        public int RetornaTotalPrimos()
        {
            return totalPrimos;
        }

        public void Adiciona1Primo()
        {
            totalPrimos++;
        }

        public void ZeraTotalPrimos()
        {
            totalPrimos = 0;
        }

        public void CronometroParallel(long _ultimo)
        {
            Console.WriteLine("Iniciar - Parallel");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Iniciar(_ultimo);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        public void CronometroNormal(long _ultimo)
        {
            Console.WriteLine("Iniciar - Normal");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Iniciar2(_ultimo);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        public void CronometroTask(long _ultimo)
        {
            Console.WriteLine("Iniciar - Task");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Iniciar3(_ultimo);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        public void Iniciar(long _ultimo)
        {
            long ultimo = _ultimo;

            int _primos = 0;
            Parallel.For(3, ultimo, z => {

                int qtd = 0;
                for (long i = 2; i < z; i++)
                {
                    if ((z % i) == 0)
                    {
                        qtd++;
                        break;
                    }
                }
                if (qtd == 0)
                {
                    Interlocked.Increment(ref _primos);
                }

            });

            totalPrimos = _primos;
        }

        public void Iniciar2(long _ultimo)
        {
            long ultimo = _ultimo;
            for (long y = 3; y <= ultimo; y++)
            {
                CalculaPrimos(y).Wait();
            }
        }


        public void Iniciar3(long _ultimo)
        {
            long ultimo = _ultimo;

            List<Task> tasks = new List<Task>();
            for (long y = 3; y <= ultimo; y++)
            {
                tasks.Add(CalculaPrimos(y));
            }

            Task.WaitAll(tasks.ToArray());
               
        }

        public int totalPrimos { get; set; } = 0;
        public async Task CalculaPrimos(long numero)
        {
            int qtd = 0;
            for (long i = 2; i < numero; i++)
            {
                if ((numero % i) == 0)
                {
                    qtd++;
                    break;
                }
            }
            if (qtd == 0)
            {
                Adiciona1Primo();
            }
        }

    }
}
