using System;

namespace ss.week3.math

{
    interface Function
    {
        double apply(double x);
        Function derivative();
        string toString();
        
    }




    class Constant : Function
    {
        public double x;
        public Constant(double x)
        {
            this.x = x;
            
        }

       public double apply(double x)
        {
            return this.x;
        }
    

       public Function derivative()
        {
            return (new Constant(0));

        }

        public string toString()
        {    
            return x.ToString();
        }

    }


    class Identity : Function
    {
          
        public double apply(double x)
        {  
            return x;
        }

        public Function derivative()
        {
            return new Constant(1);
        }

        public String toString()
        {
            return "x";
        }

    }
    
    class Sum : Function
    {
        Function g;
        Function h;
        public Sum(Function x, Function y)
        {
            this.g = x;
            this.h = y;    
        }
        
        public Function derivative()
        {
            return new Sum(g.derivative(),h.derivative());
        }

        public double apply(double x)
        {
            return g.apply(x) + h.apply(x);
        }
        
        public string toString()
        {
            return String.Format("{0} + {1}",g.toString(),h.toString());
        }

    }


    class Product : Function
    {
        Function g;
        Function h;
        public Product(Function x, Function y)
        {
            this.g = x;
            this.h = x;
        }

        public Function derivative()
        {
            return new Sum(new Product(g.derivative(),h),new Product(h.derivative(),g));
        }
        
        public double apply(double x)
        {
            return g.apply(x)*h.apply(x);
        }

        public string toString()
        {
            return String.Format("{{0}} * {{1}}",g.toString(),h.toString());
        }

    }


    class Exponent : Function
    {
        //imnplements x^n
       
       double expon;
       
       public Exponent(double exp)
        {
            this.expon = exp;

        }


        public Function derivative()
        {
            return new Product(new Constant(expon), new Exponent(expon-1));
        }


        public double apply(double x)
        {
            return expon;
        }


        public String toString()
        {
            return expon.ToString();
        }


    }


    class Program 
    {
        static void Main(string[] args)
        {
           
           Function f = new Constant(3.14159);
           Function f_prime = f.derivative();
           f.apply(5.0); // returns 3.14159
           Console.WriteLine(f.apply(5.0));
           f_prime.apply(5.0); // returns 0.0
           Console.WriteLine(f_prime.apply(5.0));

           Function f1 = new Sum( new Product(new Constant(3.0), new Exponent(2)),new Product(new Constant(2.0), new Identity()));
           Console.WriteLine(f1.derivative().toString());

        }
    }



}
