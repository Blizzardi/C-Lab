using System;

namespace ss.week3.math

{
    interface Function
    {
        double apply(double x);
        Function derivative(); 
        string toString();

    }

    interface Integrable : Function
    {
        Integrable integral();
        
    }


    class Constant : Function, Integrable
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

        public Integrable integral()
        {
            return new LinearProduct(this.x,new Identity());
        }

    }

    class Identity : Function,Integrable
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

        public Integrable integral()
        {
            return new LinearProduct(0.5,new Exponent(2));
        }

    }

    class Sum : Function,Integrable
    {
        Function g;
        Function h;

       
        public Sum(Function g, Function h)
        {
            this.g = g;
            this.h = h;
        }
        
    
        public Function derivative()
        {
            return new Sum(g.derivative(),h.derivative());
        }


        public double apply(double x)
        {
            return this.g.apply(x) + h.apply(x);
        }


        public string toString()
        {
            return String.Format("{0} + {1}",g.toString(),h.toString());
        }

        public Integrable integral()
        {
            if(g is Integrable && h is Integrable)
            return new Sum(((Integrable) g).integral(),((Integrable) h).integral());
            else
            return null;
            
        }

    }


    class Product : Function
    {
        Function g;
        Function h;

        //Integrable x;
        //Integrable y;
        public Product(Function x, Function y)
        {
            this.g = x;
            //Console.WriteLine(g.toString());
            this.h = y;
            //Console.WriteLine(h.toString());
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
            return String.Format("({0}) * ({1})",g.toString(),h.toString()); 
        }
        


    }

     


    class Exponent : Function,Integrable
    {
        //imnplements x^n
        double expon;
       public Exponent(double exp)
        {
            this.expon = exp;
        }
        public Function derivative()
        {
            //return new Product(new Constant(expon), new Exponent(expon-1));
            return new LinearProduct(expon, new Exponent(expon-1));
        }
        public double apply(double x)
        {
            return expon;
        }


        public String toString()
        {
            return "x^"+expon.ToString();
        }

        public Integrable integral()
        {
           // return new LinearProduct(expon+1,(new Constant((1/expon)+1)));
            return new LinearProduct((1/(expon+1)),new Exponent(expon+1));
        }
    

    }
    class LinearProduct : Product,Integrable
    {   
        
        Function constant;
        Function anything;
        double const1;
        public LinearProduct(double constantt, Function inter) : base(new Constant(constantt),  inter)
        {
           this.const1 = constantt;
          // this.constant = constant;
           this.anything = inter;
        }

        public Integrable integral()
        {
            if(anything is Integrable)
            return new LinearProduct(const1,((Integrable)anything).integral());
            else
            return null;
        }

        

    }

    
    

    class FindDerivateAndIntegeral 
    {

        static void Main(string[] args)
        {
           
           Function f = new Constant(3.14159);
           Function f_prime = f.derivative();
           f.apply(5.0); // returns 3.14159
          // Console.WriteLine(f.apply(5.0));
           f_prime.apply(5.0); // returns 0.0
           //Console.WriteLine(f_prime.apply(5.0));

           Function f1 = new Sum(new Product(new Constant(3.0), new Exponent(2)),new Product(new Constant(2.0), new Identity()));
           Integrable f2 = (new Constant(2.0));
           Integrable f5 = new Sum(new LinearProduct((3.0), new Exponent(2)),new LinearProduct((2.0), new Identity()));
           Console.WriteLine(f1);
           Console.WriteLine(f1.toString());
           //Console.WriteLine();
           Console.WriteLine(f1.derivative().toString());
           Console.WriteLine(f2.integral().toString());
           Console.WriteLine(f5.integral().toString());

        }
    }

}

 
    



