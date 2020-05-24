# Introduction to Scala and functional programming

## Key concepts

* Function composition
    * Easier to understand - 
    * Easier/safer to modify - 
    * Declarative rather than Imperative (describes "what" not "how")
    * Forward composition, backward composition
* Lazy evaluation
    * Programmer can focus on describing "what" to be computed [1989, Chris Reade] and not "how" to do it step-by-step
    * Compiler can conserve memory resource
    * Compiler can optimize computation (re-ordering, re-using, etc.)
* Pure functions
    * Always same results => cache friendly
    * Two pure functions are independent => easier to optimize (re-ordering, multi-threaded)
    * Referentially transparent => code does only what it says it does
    * Eliminates multi-thread bugs (race conditions, dead-locks)
    * More resilient (Spark RDD - resilient distributed dataset)
    * Program works on sinle core and also on multi-core
* Recursion and Higher-order functions
    * Replaces loops
    * Reuse of algorithm
* Currying
    * Helps define a more abstract function that can be easily specialized 
    * Helps programing with higher-order functions
    * Helps prevent specialized functions to be individually defined that leads to code clutter
* Partial application
    * Helps define a specialized functions from another more abstract functions
    * Reduces 

# Part 1 Function composition

## Preparation

We will use scala REPL environment. 

- Install JDK (Open JDK will do). Look for a zip file http://jdk.java.net/12/, and unzip it (may need to set up a couple of environment variables)
- Install Scala. Go to https://www.scala-lang.org/download/ and look for a Windows installer (msi).

Once installed, run ```scala.exe``` at your arbitrary working directory. Scala REPL environment will open up. All of the exercises are done via Scala REPL.

Many experts seem to agree that **function composition** is one of the most useful and powerful feature of functional programming paradigm. 

In this section, we will implement a simple multi-step process in two different styles: imperative and functional programming style.

The steps for our process are: 
1. Take a string representation of an integer, which may have a leading minus sign, and convert it to an Int value
1. Get the absolute value of the Int value
1. Add 1 to the value

For example, if we pass ```"-20"``` to this process, it should be first converted to ```-20```, then ```20```, and then add ```1```, which resuts in value ```21```.

## Imperative programming style

First, let's quickly review how to perfom each step in Scala, using intrinsic expression as well as some library functions.

```scala
scala> Integer.parseInt("-20")
scala> Math.abs(-20)
scala> 20 + 1
```

Now, define a function that executes these functions as sub-routines.

```scala
scala> def f(s:String):Int = {
     | val i = Integer.parseInt(s)
     | val a = Math.abs(i)
     | val r = a + 1
     | r
     | }
```

The syntax is very similar to any C-derived programming language (e.g. Java, C#, JavaScript, etc.), so anyone with experiences in these langauges should
have no problem understanding it.

Note that Scala REPL will automatically add a pile character ```|``` when a expression is incomplete. To finishi up, complete the expression and keep pressing Enter key until Scala REPL takes it. 

Invoking a function is straightforward as well.

```scala
 scala> f("-20")    
```

Alternatively, you could also define the function with all of the inputs and outputs of functions and expressions as *inline*.

```scala
scala> def f = (s:String) => Math.abs(Integer.parseInt(s)) + 1
```

So far, these codes do not really use functional programming feature, other than using lambda expression (which really just means annonymous functions here). Scala is *not* a pure functional programming language - it is function-programming-first language, so you can still do imperative programming with it.

## Each step is a function

Before we turn to examine functional programming counterpart, let us be sure to understand that each of the three steps are, and can be represented as a  function, including the ```a + 1``` arithmetic expression. This is important because functions are building blocks of function composition.

Before we look at ```Integer.parseInt```, let's see ```Math.abs``` function (```Integer.parseInt``` is an overloaded function so its a bit complicated. We will see it next.)

```scala
scala> Math.abs _
res46: Int => Int = $$Lambda$1088/0x0000000801835040@5d5db92b
```

Note that specifing an underscore character ```_``` to a function prevents *execution the function*, instead it will *return the type of function*. Without the underscore, the interpreter will try to run it and will complain about missing argument.

```Int => Int``` part reads as *"a function that takes an argument of Int type and returns a value of Int type."*

The right-hand side of ```=``` is a representation of the actual body of the function, and the ID is unique to my particular environment so you can ignore it.

Next, let's take a look at ```Integer.parseInt``` function. 

```scala
scala> Integer.parseInt _
               ^
       error: ambiguous reference to overloaded definition,
       both method parseInt in class Integer of type (x$1: String)Int
       and  method parseInt in class Integer of type (x$1: CharSequence, x$2: Int, x$3: Int, x$4: Int)Int
       match expected type ?
```

As you see above, we get some error. The compiler is complaining that it is an overloaded function, and since it lacks argument information it is ambiguous and cannot resolve it to a specific overloaded function.

To resolve the ambiguity, we specify the argument list for the one we want (in this case, we want the one that takes a single String argument).

```scala
scala> Integer.parseInt(_: String)
res47: String => Int = $$Lambda$1089/0x0000000801834840@4fca434c
```

```String => Int``` part reads as *"a function that takes an argument of String type and returns a value of Int type"*

Finally, we look at the expression ```a + 1```, where ```a``` is an unknown value. Because we can't specify any real value, we turn it into a Lambda expression.

```scala
scala> (a: Int) => a + 1
res48: Int => Int = $$Lambda$1090/0x0000000801840840@5d8f3a36
```

Because a lambda expression is an annoymous function, Scala REPL shows a function type.

We are now certain that all three steps can be representd as *functions*.

The main goal of this section was to understand that all these steps can be represented as "functions". That means we can individually pass around these functions, replace with others, include or exclude in/from a groupd of functions, etc. When a language allows to do this, the functions are said to be *first-class functions*.

## Functional programming style

Now we will look at how to implement the same function using functional style of programming.

First, we will give names to the three sub-routines (functions). This is not technically necessary but helps reduce some clutter when we try to understand function componsion code. Later, we will remove this unnecessary indirection.

```scala
scala> def s1 = Integer.parseInt(_: String)
scala> def s2 = Math.abs _
scala> def s3 = (a: Int) => a + 1
```

This way, we have names ```s1```, ```s2```, and ```s3``` for these sub-routines.

Next, we will compose these sub-routines to form a new function, named ```f2```.

```scala
scala> def f2 = s1.andThen(s2).andThen(s3)
```

```andThen``` is a method of Scala's **Function object**. It pipes a function's output to another function's input, as if they are arranged like this:

```
s3(s2(s1(x)))
```

You can imagine an execution starts from the inner-most function, ```s1```, taking ```x```, and then passes the result to ```s1```, and move on.

Alternatively, you can compose using a different method called ```compose```. Below are examples with the same effect, with two different syntax.

```scala
// pattern 1
scala> def f2 = s3.compose(s2).compose(s1)
// patter n2
scala> def f2 = s3 compose s2 compose s1
```

This might be confusing, particulary when you think about where the third ```s1``` is connected to first pair. Here is a trick for how to read it.

When you look at ```s3.compose(s2)``` and ask yourself "which function will be called first?" then the answer is ```s2```. That means this composed function, when invoked, will first run ```s2```. So the seconf ```compose``` is applied on ```s2``` as if ```s2.compose(s1)``` is called. Thus, it will be ```s3(s2(s1(x)))```.

Going back to the discussion of ```f2```, our composed function, note that ```f2``` here is *just a function definition*. We have not yet invoked it.

So now, We finally invoke it as shown below. We pass "-20" as the argument, and we expect value ```21``` to be returned.

```scala
scala> f2("-20")
```

As mentioned earlier, ```s1```, ```s2```, and ```s3``` are just aliases to library functions, and an expression, so we can compose a function without using aliases:

```scala
// single line version
scala> def f3 = (Integer.parseInt(_:String)).andThen(Math.abs _).andThen((x:Int) => x + 1)

// multi line version
scala> def f3 = (Integer.parseInt(_:String)).
     | andThen(Math.abs _).
     | andThen((x:Int) => x + 1)
```

As you can see, we simply substitute each step with the actual function definition.

# What are the merits of function composition using functional programming style?

There are a number of advantages to function composition using functional programming over imperative function defintions.

## Merit: Easier and safer to change

Let's take a look at an example. We will change the requirements for our process, update both imperative and functional implementation accordingly, and then compare and evaluate them.

Our original processing logic was as follows:

1. Take a string representation of an integer, which may contain a minus sign, and convert it to an Int value
1. Get the absolute value of the Int value
1. Add 1 to the value

Imagine we have a new requirement where we need to log the intermediate values for some debugging purpose. The requirement would change to:

Requirement: 
1. Take a string representation of an integer, which may contain a minus sign, and convert it to an Int value
1. **[New]** Log the original Int value
1. Get the absolute value of the Int value
1. **[New]** Log the absolute value
1. Add 1 to the value
1. **[New]** Log the final value

Note: In genaral, any I/O is considered "side-effect" and some languages like Haskell manages I/O in a special way to maintain its *pure* functional principle. Logging is used here for illustration purpose and deos not suggest any best practice for logging.

Now, let's compare how we implement this new requirement in both imperative style and functional style.

### Imperative programming style

```scala
scala> def f(s:String):Int = {
     | val i = Integer.parseInt(s)
     | println(i)
     | val a = Math.abs(i)
     | println(a)
     | val r = a + 1
     | println(r)
     | r
     | }

 scala> f("-20")
-20
20
21
res55: Int = 21  
```

Note that we had to directly modify the body of function ```f``` and inserted new statements, in this case call to ```println``` function. 

Also note that our new code interacts with local variables (```i```, ```a```, and ```r```). We were lucky that the intermediate values were already represented as variables but if they were passed as inline values, we would have had to define new local variables, making changes to existing code. This means sometimes changing the behavior of an existing function requires unplanned refactoring (and we all know that refactoring is a potential cause of regression!)

Obviously, we have to re-test the function ```f``` to make sure that we didn't mess up with anything while modifying the body.

### Functional programming style

Before we modify the code that composes functions in functional programming style, we need to define a new function that prints out a value to console and returns a value. This is necessary becasue the built-in ```println``` function does not return a value, preventing us from **chaining** it with other functions.

A simple wrapper function ```printInt``` is sufficient.

```scala
scala> def printInt(x:Int):Int = {
     | println(x)
     | x
     | }
```

Once we prepared a wrapper ```printInt``` function, we can insert it to the pipeline of the functions. Because ```printInt``` has **identity** property (e.g. returns the input as is, like passing 1 returns 1, passing 32 returns 32), it is safe to insert it into any section of the pipeline.

```scala
scala> def f3 = (Integer.parseInt(_:String)).andThen(printInt).andThen(Math.abs _).andThen(printInt).andThen(_ + 1).andThen(printInt)

scala> f3("-20")
-20
20
21
res31: Int = 21
```

Alternatively, using aliases for sub-routines:

```scala
scala> def f = s1.andThen(printInt).
     | andThen(s2).andThen(printInt).
     | andThen(s3).andThen(printInt)
```

Because the functions we used to compose are all independent and tested (these are library and language intrinsic expression), the only thing we have to guarantee is that the inserted printInt is "pure" (meaning it has no side-effect business logic wise, and the same value is always returned for a particular input value, which is the case because printInt is "identity" function). If it has no side-effect, how could it cause the existing logic to break? Can't.

## Merit: More reusable

Functions can be reused in different situations by being included in many different compositions.

For example, we can enlist functions to be composed as a named collection:

```scala
scala> val func = List(s2, s3)
```

Because ```func``` is just a list of functions, you can add more functions, reorder them (certain restrictions apply) and remove some if not needed.

Below, we compose a new function using one of the many **higher-order functions** available in Scala. (A higher-order function is a function that can take functions as arguments and can return a function).

```scala
scala> def fc = func.foldLeft(s1)((ac, cu) => ac.andThen(cu))
scala> fc("-20")
res64: Int = 21
```

```foldLeft``` takes an *initial input value*, in this case a function that converts String to Int, and then starts passing a pair, starting with the initial input and the first element in the collection, and *chains them together*. It continues on to the next pair, with one being the "chained functions" and the other being the subsequent element in the collection. 

The final result is a **chained** functions, or a composed function.

Now, let us demonstrate how functions can be more easily and flexibly reused. If we want to add "logging" functionality to the same function, we can add ```printInt``` functions to the list like this:
scala

```
scala> val funs = List(printInt _, s2, printInt _, s3, printInt _)
scala> def fc = funs.foldLeft(s1)((c, n) => c.andThen(n))

scala> fc("-20")
-20
20
21
res66: Int = 21
```

Notice that now the intermediate values are printed out to the console.

Alternatively, we can make it more readable if we have an alias that represents ```printInt _``` function type.

```scala
scala> def prn = printInt _
scala> def func2 = List(prn, s2, prn, s3, prn)
scala> def fc = func2.foldLeft(s1)((ac, cu) => ac.andThen(cu))
```

## Merit: Easier to specialize from abstract functions

Like ```foldLeft```, many other higher-order functions are available in a functional programming languages like Scala. This type of function takes functions as arguments and operate on elements in a collection. Some common examples are: ```map```, ```reduce```, ```fold```, etc. (For C# programmers, LINQ's ```Select``` and ```Aggregate``` are approximate counterparts)

With function composition, it is easier to create specialized functions that can be used together with higher-order functions.

Here is a sample requirement for processing a collection of data:

1. Given an array of strings (e.g. "-20", "32", "-100")
1. Generate a result set that transforms each value to an absolute value and add 1.
1. Also, generate a result set that transforms each value to an absolute value and multiply by 2

Note that we want two result sets, and they differ only by either incremented by 1 or multiplied by 2.

This can be achieved by using a higher-order function ```map``` twice: once for the incremented result and the other for multiplied result.

### Imperative programming style

Typically, we create named functions that have slightly different business logic. (NOTE: there are many techniques to refactor these variations using common OOP design patterns but we will assume simple implementation)

```scala
// define an input array
scala> def ary = List("-20", "32", "-100")

scala> def add1(s:String):Int = {
     | val i = Integer.parseInt(s)
     | val a = Math.abs(i)
     | val r = a + 1
     | r
     | }

scala> def mulBy2(s:String):Int = {
     | val i = Integer.parseInt(s)
     | val a = Math.abs(i)
     | val r = a * 2
     | r
     | }

scala> ary.map(add1)
res71: List[Int] = List(21, 33, 101)

scala> ary.map(mulBy2)
res72: List[Int] = List(40, 64, 200)
```

Note that you will have to define two specialized functions, ```add1``` and ```mulBy2```.

You could use lambda expression (anonymous function) instead of named functions in place of call to ```add1``` and ```mulBy2```, but you would still have to repeat most of the business logic, unless you decompose it to smaller named functions, which goes back to the same problem of "many named functions".

### Functional programming style

Using function composition, a specialized function is created on the fly and used only once and discarded, at each call site.

```scala
scala> def ary = List("-20", "32", "-100")

scala> ary.map((Integer.parseInt(_:String)).andThen(Math.abs _).andThen(_ + 1))
res73: List[Int] = List(21, 33, 101)

scala> ary.map((Integer.parseInt(_:String)).andThen(Math.abs _).andThen(_ * 2))
res74: List[Int] = List(40, 64, 200)
```

Let's see additional examples of specializations.

```scala
// Change ordering
scala> ary.map((Integer.parseInt(_:String)).andThen(_ + 1).andThen(Math.abs _))

// Add 1 and multiply by 2
scala> ary.map((Integer.parseInt(_:String)).andThen(Math.abs _).andThen(_ + 1).andThen(_ * 2))

// Compose a common function
scala> def p = (Integer.parseInt(_:String)).andThen(Math.abs _)
p: String => Int

// Reuse the common function function
scala> ary.map(p.andThen(_ + 1))
res76: List[Int] = List(21, 33, 101)

scala> ary.map(p.andThen(_ * 2))
res77: List[Int] = List(40, 64, 200)

```

As you might imagine that experimentation is easier. This has some impact to productivity when developers use REPL (Read-Evaluate-Print-Loop) workflow.

By the way, in the above example, one of the other powerful concept known as *partial application* is demonstrated. Partial application will be discussed next.

# Part 2

# Partial application and currying

The notion of *partial application* and *currying* can be confusing but they are different. Partial application is possible without currying, and curried functions does not necessarily need to be partially applied. Oftten times, they are used together, but one does not depend on the other.

For now, we forget about *currying* concept (will cover later) and just look at partial application used with a traditional multi-argument functions (non-curried functions). Then, as a preparation, we will see how we can define curried functions. After that, we will look at *partial application used with curried functions*. Finally, we will look at function composition with curried functions.

Higher-order functions are often the primary context at which partially applied functions and function composition is used (with or without curried functions involvement). Higher-order functions deserves its own section, and discussed later.

## Create specialized functions from multi-argument functions

With partial application feature, we can create a new function from another by specifying some of the arguments needed to perform evaluation of the expression.

We will first look at how to use this feature with functions that take more than one argument.

Let's create a simple function ```f``` with three arguments.

```scala
def f(a: Int, b: Int, x: Int):Int = a * x + b
```

You can invoke the function as you would expect.

```scala
scala> f(2, 5, 3)
res81: Int = 11
```

Now, what if ```a``` is known and should be 2```2```, then you can do this.

```scala
scala> def f2 = f(2, _, _)

scala> f2(5, 3)
res85: Int = 11
```

We use underscores here to represent unknown arguments ```b``` and ```x```. The value for ```a``` has been *partially applied* to ```f``` and the resulting function is created as ```f2```. We call ```f2``` by passing two arguments, one for ```b``` and the other for ```x```.

Let's consider another scenario: when we only know a value for ```b``` (say  ```5```) 

You probably guessed it right.

```scala
scala> def f3 = f(_, 5, _)
f3: (Int, Int) => Int

scala> f3(2, 3)
res84: Int = 11
```

Note that ```f3``` takes two arguments for ```a``` and ```x```. The value ```5``` has been already applied, so it just needs the reamining arguments.

At this point, it should be obvious how we can supply two arguments for ```a``` and ```b```.

```scala
scala> def f4 = f(2, 5, _)
f4: Int => Int

scala> f4(3)
res86: Int = 11
```

## Preparing curried functions

First, let's look at the same function discussed above, but in *curried form*.

Note: It is worth emphasising this point that currying itself has nothing to do with partial application. It *may* be used with partial application feature, but there is no dependencies between them.

```scala
scala> def fc(a:Int)(b:Int)(x:Int) = a * x + b
fc: (a: Int)(b: Int)(x: Int)Int
```

The actual operation of ```fc``` is exactly the same as the multi-argument version ```f```.

One difference you would recognize is the way argument list is specified. Each argument, ```a```, ```b```, and ```c```, are enclosed individually in a sparate pair of parenthesis. This causes ```fc``` to take a single argument ```a``` and returns another function.

To illustrate this, let's compare the function signature of multi-argument function ```f``` and curried function ```fc```.

```scala
scala> f _
res96: (Int, Int, Int) => Int = $$Lambda$1172/0x0000000801894040@ffa7bed

scala> fc _
res97: Int => (Int => (Int => Int)) = $$Lambda$1173/0x0000000801893840@4e843bf6
```

The multi-argument function is straightforward. ```(Int, Int, Int) => Int``` part says *"it is a function that takes three arguments, all Int type, and returns a value of Int type"*.

The curried (single-argument) function is very different: ```Int => (Int => (Int => Int))``` says *"it is a function that takes a single argument of Int and returns a function that takes a single argument of Int type and returns a function that takes a single argument of Int type and returns a value of Int type."*. Wow, that was long.

The key idea here is the phrase "...takes a single argument..." in the above statement appears as many times as arguments.

So, if we invoke the function ```fc``` by passing a single argument, which is perfectly legal as it can only take a single argument anyway, it will return a function whose signatur look like ```(Int => (Int => Int))```.

With a curried function, you would never pass multiple arguments to any part/level of (nested) functions.

**Curried function example in JavaScript**

We can see the same sample curried function in JavaScript (I used Google Chrome Developer tool console).

```javascript
> var fc = a => b => x => a * x + b
< undefined
> fc
< a => b => x => a * x + b
```

Perhaps, seeing it in a different forat that uses ```function``` keyword is easier to see what's going on.

```javascript
var fc3 = function(a) { 
    return function(b) { 
        return function(x) { 
            return a * x + b 
            } 
        } 
    }
```

Note that the value for argument ```a``` is available to the nested function *by closure*, and so is ```b```. That is how the expression ```a * x + b``` can obtain the value for ```a``` and ```b```. Closure is an enabling feature for currying in JavaScript.

Both ```fc``` and ```fc3``` are functionally the same. Below, we invoke the two functions with the same list of arguments.

```javascript
fc(2)(5)(3)
11
fc3(2)(5)(3)
11
```

Note that because curried function take a single argument, at every level, we must pass a series of one arguments. Hence, ```(2)```, ```(3)```, and finally ```(3)```.

Let's leave JavaScript world and come back to Scala world. Here is how we invoke the curried version of function  

```scala
scala> fc(2)(5)(3)
res99: Int = 11
```

There is no suprise that the syntax, in terms of how we pass a series of single-arguments. It is the same as in JavaScript.

**Converting multi-argument function to curried function**

Wouldn't it be nice if there is an easy way to turn a multi-argument function into curried function? Luckly, Scala has a built-in mechanism for that.

Below, we convert ```f``` that takes three arguments, into a function ```fc``` that is already curried.

```scala
scala> def fc = (f _).curried
fc: Int => (Int => (Int => Int))

scala> fc(2)(5)(3)
res103: Int = 11
```

```(f _)``` part represents the function ```f``` - remember we specify an underscore to prevent interpreter from trying to invoke the function. You can access the member ```curried``` to obtain a curried version of the function.

## Create specialized functions from curried functions

At this point, discussion of creating specialized function from curried function is redundant as you have already seen some examples.

Below, we first create a new function ```fc4``` that partially apply actual values for argument ```a``` and ```b```, but leave ```x``` as missing argument. Then, we invoke ```fc4``` while supplying the actual value for argument ```x```.

```scala
scala> def fc4 = fc(2)(5)
fc4: Int => Int

scala> fc4(3)
res104: Int = 11
```

## Compose functions using curried functions

Earlier we looked at how to create a new function by function composition techunique using ```andThen``` method. We also looked at how to do that using ```compose``` method. 

Let's review it again. 
Given two functions:

```scala
scala> def inc(x:Int) = x + 1
scala> def mulBy2(x:Int) = x * 2
```

Say we want to form a function as ```mulBy2(inc(x))```. For example, when ```x=1```, the result should be ```4```; when ```x=2```, the result should be ```6```.

Here is how to compose a new function using ```compose``` method.

```scala
scala> def c1 = (mulBy2 _).compose(inc _)
```

You can call c1 with actual values:

```scala
scala> c1(1)
res206: Int = 4

scala> c1(2)
res207: Int = 6
```

Similarily, if you want to compose the same functions as ```inc(mulBy2(x))```, so that ```x=1``` would result in ```3``` and ```x=2``` result in ```5```, then you would do:

```scala
scala> def c1 = (inc _).compose(mulBy2 _)
```

Basically, the *composed function* is called and its output is passed as the input of the *composer/composing function*.

With this new tool, we want to compose a function using ```fc``` that was earlier defind like this:

```scala
def fc(a:Int)(b:Int)(x:Int) = a * x + b
```

Note the fact that it takes three arguments.

Now, let's setup some context for requirement: what if we want to have some pre-processing operations for ```a``` and ```b``` as follows:

- A value of ```a``` should be converted to an absolute value (hint: Math.abs library function)
- A value of ```b``` must be not be greater than 5 (hint: Math.min library function)

To represent these conditions, we can use the following two expressions:

```scala
Math.abs _
def atMost5 = Math.min(5, (_:Int)) // a helper function
```

Here is a quick test for ```atMost5``` helper function (incidentally, we use partial application for this)

```scala
scala> (1 to 10) map(atMost5)
res8: IndexedSeq[Int] = Vector(1, 2, 3, 4, 5, 5, 5, 5, 5, 5)
```

Note that 5 is the max value when input is greater than 5.

Now, we will compose a function that perform the pre-processing operations for ```a``` and ```b```, in step by step manner. (we will combine them all later)

```scala
scala> (fc _).compose(Math.abs _)
res10: Int => (Int => (Int => Int)) = // ommitted
```

This would logically look like ```fc(Math.abs(a))```. It is still a function that takes *three* arguments - not *two*. It is *very important* to understand that we have not yet applied any actual argument yet. We simply made sure that an argument that will come later will *pass through* ```Math.abs``` function so that any negative value would become a positive value.

Next, we need to make sure that the *second* argument, ```b```, is at most ```5``` by making sure the actual value will *pass through* our helper function ```atMost5```. In order to insert this helper function for *the second argument*, we have to somehow *skip* the first argument by using underscore.

```scala
scala> (fc _).compose(Math.abs _)(_:Int).compose(atMost5)
```

Notice that ```(_:Int)``` immediately follows ```(fc _).compose(Math.abs _)``` part. This effectively skips the first argument, and brings the second argument into the spotline, if you will.

 Let's give a name to this composed function so that we can call it separately.

```scala
scala> def fcx = (fc _).compose(Math.abs _)(_:Int).compose(atMost5)
fcx: Int => (Int => (Int => Int))
```

We have a composed function ```fcx``` that should still take *three* arguments. Remember we have not yet specified any actual argument to the original function ```fc```. (We did use partial application feature for our helper function ```atMost5``` but that has nothing to do with ```fc```).

We composed a function such that two arguments will pass through two pre-processing routines, *without* making any changes to the original function ```fc```. Imagine how you can do this in imperative programing style. 

Probably you can do that using inline Lambda expressions but whether you want to do that is debatable.

Now, we would expect ```fcx``` to perform pre-processing operations against value of ```a``` and ```b``` such that we can expect that following test cases would hold.

- ```fcx(2)(5)(1)``` should be ```2 * 1 + 5```, so the result should be ```7```.
- ```fcx(-2)(5)(1)``` should be ```2 * 1 + 5```, where ```-2``` was converted to ```2``` because it passed through ```Math.abs``` function. The result should be ```7```.
- ```fcx(2)(8)(1)``` should be ```2 * 1 + 5```, where ```b=8``` was changed to ```b=5``` because our helper function ```atMost5``` forced it to be ```5```. The result is still ```7```. 

Here is tests:

```scala
scala> fcx(2)(5)(1)  
res27: Int = 7       
                     
scala> fcx(-2)(5)(1) 
res28: Int = 7       
                     
scala> fcx(-2)(8)(1) 
res29: Int = 7       
```

It is working as expected.

## What is the advantage of curried functions

If you search for the answer to this question online, there are mixed opinions, and many of them seem to incorrectly specify the benefit of *partial application* as if it is the benefit of *currying*. However, curried functions is not a requirement for partial application, so it can't be the benefit of curried functions.

One article that seems to actually explain the benefit of curried function is this [article by Iven Marquardt](https://medium.com/@iquardt/currying-the-underestimated-concept-in-javascript-c95d9a823fc6), which basically point out that curried function *abstracts the variations of number of arguments*, that would improvet the likelyhood of curried function being more (re)usable in more situations - with function composition, partial application, and both.

In the next section, we will discuss higher-order functions that are the most useful context in which many of the functional programming feature start paying off its benefits.

# Part 3 Higher-order functions

Higher-order functions have become very popular among not only in functional programming languages but also in imperative languages. For instance, C# added LINQ with functions like Select, Where, and Aggragate.

When you use higher-order functions in your code, you are essentially doing *function composition*. For example, using one of the common higher-order function *map*, you apply the same transformation to all elements in a collection by passing a function.

In the following example in Scala, we use *map* with *inc* (increment) function to transform an array of Ints to be all incremented.

```scala
scala> def inc(x: Int) = x + 1
inc: (x: Int)Int

scala> def list = List(3, 5, 1, 7)
list: List[Int]

scala> list.map(inc)
res0: List[Int] = List(4, 6, 2, 8)
```

The follow function are common higher-order functions that operate on a collection.

| Function | what it does |
| :---- | :---- |
| map | transforms each element |
| reduce | aggregate elements |
| fold | aggregate with some initial value |
| fold left | fold starting with the left-most element |
| fold right | fold starting with the right-most element

Let's look at one function at a time, starting wiht ```reduce``` which takes a function with two arguments that represent two randomly selected elements from the collection, and pass them to the specified *operation*, until all elements are selected.

```scala
// sum using reduce
scala> Seq(3, 5, 1, 7).reduce((a, b) => a + b)
res47: Int = 16

// product using reduce
scala> Seq(3, 5, 1, 7).reduce((a, b) => a * b)
res49: Int = 105
```

Using ```reduce```, we obtained sum and product of a sequence of integers.  

Next, let's look at ```fold``` for collection, which takes an initial value, and a function that takes two argument, one for the intermediate value and the other for an element. 

```scala
// sum using fold
scala> List(3, 5, 1, 7).fold(0)((ac, x) => ac + x)
res40: Int = 16

// product using fold (with bug)
scala> List(3, 5, 1, 7).fold(0)((ac, x) => ac * x)
res41: Int = 0

// product using fold (without bug)
scala> List(3, 5, 1, 7).fold(1)((ac, x) => ac * x)
res42: Int = 105

```

You notice that examples include two ways to obtain a *product*, one with a bug and the other one that works. This is to illustrate that the first argument to fold is used as an initial value: obviously, if we pass ```0``` as an initial value for a product of values, it will be always ```0```. You would have to pass ```1``` instead. 

> ```fold``` is an interesting function that has different use for other types called Option and Either (which is not discussed here). The example presented here is only for the ```fold``` for collections

Finally, we will look at ```foldLeft```. The significance of ```foldLeft``` is to evaluate elements from left-to-right order.

```scala

// demo order of elements with foldLeft
scala>  List(3, 5, 1, 7).foldLeft(0)((ac, x) => { println(ac); ac + x })
0
3
8
9
res51: Int = 16
```

In the above example, intermediate/accumulator value is printed out to show that elements are selected from left to right order.

For a comparison, here is ```foldRight``` that shows that element is picked from right-to-left order.

```scala
scala> List(3, 5, 1, 7).foldRight(0)((x, ac) => { println(ac); ac + x })
0
7
8
13
res2: Int = 16
```

## Higher-order functions in different languages

Here is a table to common higher-order functions in a several popular languages.

| | Scala | JavaScript | C#/LINQ |
| :----| :---- | :---- |:---- |
| Apply an operation to all elements| map | map |Select |
| Combine multiple vectors into one | reduce | reduce | Aggregate |
| Combine multiple vectors into one, with an initial value| fold | reduce | Aggregate |
| Returns elements that satifies a predicate | filter | filter | Where |

There are differences among different language implementations (e.g. Scala's reduce is not ordered) so should check each language references for details.

