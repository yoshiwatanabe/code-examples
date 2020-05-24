# Introduction

Scala REPL environment (REPS == Read-Evaluate-Print-Repeat)

# Function composition

> How do we compose functions in Scala? How is it different from traditional/imperative function definitions?


```scala
scala> Integer.parseInt("-20")
scala> Math.abs(-20)
scala> 20 + 1
```

```scala
scala> def f(s:String):Int = {
     | val i = Integer.parseInt(s)
     | val a = Math.abs(i)
     | val r = a + 1
     | r
     | }
```

```scala
 scala> f("-20")    
```

```scala
scala> def f = (s:String) => Math.abs(Integer.parseInt(s)) + 1
```

For convenience, let's efine aliaes for sub-routines

```scala
scala> def s1 = Integer.parseInt(_: String)
scala> def s2 = Math.abs _
scala> def s3 = (a: Int) => a + 1
```

Special handling of overloaded functions
```scala
scala> Integer.parseInt _
               ^
       error: ambiguous reference to overloaded definition,
       both method parseInt in class Integer of type (x$1: String)Int
       and  method parseInt in class Integer of type (x$1: CharSequence, x$2: Int, x$3: Int, x$4: Int)Int
       match expected type ?
```

```scala
scala> Integer.parseInt(_: String)
res47: String => Int = $$Lambda$1089/0x0000000801834840@4fca434c
```

Convert an expression to Lambda function

```scala
scala> (a: Int) => a + 1
res48: Int => Int = $$Lambda$1090/0x0000000801840840@5d8f3a36
```

> There are two ways to compose using Scala's built-in methods

Compose functions using ```andThen``` method.

```scala
scala> def f2 = s1.andThen(s2).andThen(s3)
```

```
s3(s2(s1(x)))
```

Compose functions using ```compose``` method
```scala
// pattern 1
scala> def f2 = s3.compose(s2).compose(s1)
// patter n2
scala> def f2 = s3 compose s2 compose s1
```

Invoke

```scala
scala> f2("-20")
```

Without alias

```scala
// single line version
scala> def f3 = (Integer.parseInt(_:String)).andThen(Math.abs _).andThen((x:Int) => x + 1)

// multi line version
scala> def f3 = (Integer.parseInt(_:String)).
     | andThen(Math.abs _).
     | andThen((x:Int) => x + 1)
```

> Flexibile, easy to change, safer to change

How you do in function definition

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

How do you do in function compositin.

... But first we need to aligh functions to be "composition-friendly"

```scala
scala> def printInt(x:Int):Int = {
     | println(x)
     | x
     | }
```

Using sub-routine aliaes

```scala
scala> def f = s1.andThen(printInt).
     | andThen(s2).andThen(printInt).
     | andThen(s3).andThen(printInt)
```

Using sub-routines exploded

```scala
scala> def f3 = (Integer.parseInt(_:String)).andThen(printInt).andThen(Math.abs _).andThen(printInt).andThen(_ + 1).andThen(printInt)

scala> f3("-20")
-20
20
21
res31: Int = 21
```

Functions are just objects in Scala - first-class object.

List of functions to be composed

```scala
scala> val func = List(s2, s3)
```

Compose the list of functions.

```scala
scala> def fc = func.foldLeft(s1)((ac, cu) => ac.andThen(cu))
scala> fc("-20")
res64: Int = 21
```

Now, easy to modify the funtions to be composed.

```scala
scala> def prn = printInt _ // create alias
scala> def func2 = List(prn, s2, prn, s3, prn)
scala> def fc = func2.foldLeft(s1)((ac, cu) => ac.andThen(cu))
```

# Partial applications

Example multi-argument function

```scala
def f(a: Int, b: Int, x: Int):Int = a * x + b
```

```scala
scala> f(2, 5, 3)
res81: Int = 11
```

Partially applied functions

```scala
scala> def f2 = f(2, _, _)

scala> f2(5, 3)
res85: Int = 11
```

```scala
scala> def f3 = f(_, 5, _)
f3: (Int, Int) => Int

scala> f3(2, 3)
res84: Int = 11
```

```scala
scala> def f4 = f(2, 5, _)
f4: Int => Int

scala> f4(3)
res86: Int = 11
```

## Curried functions

Defintion of currying

Example

```scala
scala> def fc(a:Int)(b:Int)(x:Int) = a * x + b
fc: (a: Int)(b: Int)(x: Int)Int
```

Convert muti-argument functions into curried fuctions

```scala
scala> def fc = (f _).curried
fc: Int => (Int => (Int => Int))

scala> fc(2)(5)(3)
res103: Int = 11
```

**Curried function example in JavaScript**


```javascript
> var fc = a => b => x => a * x + b
< undefined
> fc
< a => b => x => a * x + b
```

```javascript
var fc3 = function(a) { 
    return function(b) { 
        return function(x) { 
            return a * x + b 
            } 
        } 
    }
```

```javascript
fc(2)(5)(3)
11
fc3(2)(5)(3)
11
```

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

```scala
def fc(a:Int)(b:Int)(x:Int) = a * x + b
```

Additional constraints

- A value of ```a``` should be converted to an absolute value (hint: Math.abs library function)
- A value of ```b``` must be not be greater than 5 (hint: Math.min library function)

```scala
Math.abs _
def atMost5 = Math.min(5, (_:Int)) // a helper function
```

Test

```scala
scala> (1 to 10) map(atMost5)
res8: IndexedSeq[Int] = Vector(1, 2, 3, 4, 5, 5, 5, 5, 5, 5)
```

Step 1

```scala
scala> (fc _).compose(Math.abs _)
res10: Int => (Int => (Int => Int)) = // ommitted
```

Step 2

```scala
scala> (fc _).compose(Math.abs _)(_:Int).compose(atMost5)
```

Final composed function

```scala
scala> def fcx = (fc _).compose(Math.abs _)(_:Int).compose(atMost5)
fcx: Int => (Int => (Int => Int))
```

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