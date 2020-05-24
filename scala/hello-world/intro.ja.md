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


# 目的

私は普段仕事ではC#を使います（95%以上。時々JavaScript)、ですがデータ処理のためにScala を使う案件が出てきそうな雰囲気になってきたので、ここはひとつ「ScalaのもっともScalaらしい、しかも最重要な機能を学ぼう」ということになり、その学習というか調査の結果を、主に関数型プログラミングにあまり縁の無い方や、すこしかじったけどよくその良さが分からない（つまり自分！）を対象にまとめました。元々は英語で書いたんですが、頑張って日本語にしました。

# おことわり

関数型プログラミングに関しては、興味レベルでHaskellについてちょっと本を読んだり、F#でごく簡単なツールを実装した、ぐらいの経験で、実務経験はほぼ０です。ですが、関数型プログラミングの方法論やベストプラクティスはじわじわと非関数型プログラミング言語・環境でも見るようになってきました（例えばASP.NETのミドルウェアなどは一種の関数合成でしょう）。また、関数型プログラミング言語でなくても、関数型プログラミングが推奨するコーディング（例えば副作用を減らす、参照透過性を目指すなど）はベストプラクティスとして知っておくべきだと思うようになりました。（本音を言うと、関数型プログラミング、例えばf#を使ってプロジェクトをやりたいんですよね・・・）

# 内容

今回は、Scala と関数型プログラミングの「ど真ん中」を攻めたいと思いました。Scalaと関数型プログラミング言語を取り巻く様々な概念の中から、特に重要で「それがあるからこそ関数型プログラミング！」といえるような概念に集中することにしました。ですから、lazy evaluation, pure function, pattern matchin, referential transparency, といった概念は全く扱わないか、副次的な位置づけにしました。

自分なりに調査して、もっとも重要だと感じたのが、重要な順から並べて、以下の項目になります（あくまでも個人の意見です）。

- 関数合成（パート１としてこの投稿の下にあります）
- 部分適用とカリー化（パート２として別の投稿にあります）
- 高階関数（まだ書きかけです。map, reduce, fold, などに付いて）

# こんな人にオススメ（たぶん）

- 普段、関数型プログラミングを使わないひと
- 関数型プログラミングっていったい何なのって疑問に思ってるひと
- Javaを普段使いだけどScalaが気になってる人（環境的には一番楽な人）
- 関数型プログラミングのいったいどこがどのよういいいのか知りたい人
- 関数型プログラミングの「カリー化」がいまいちよく分からない人
- 手で触りながら勉強したい人（全ての例はScalaのコンソールに打ち込めます）

# Part 1 関数合成

## 準備

ScalaをREPLできる環境が必要です。 

- JDKをインストールします (Open JDK でもオッケー)。http://jdk.java.net/12/ にあるZipファイルがありますから、適当な場所に解凍しましょう (例によって環境変数をちょっといじることになるでしょう。詳しくはJDKインストール手順を解説した記事をみてください)
- Scalaをインストール。 リンクは https://www.scala-lang.org/download/ です。僕はウィンドウズユーザーなので、Windowsインストーラー (msi)を使いました。

インストールが完了したら、コマンドラインから ```scala.exe``` を実行します。Scala REPL環境が出来ます。この記事のエクササイズはすべてScala REPL環境でやりました（もちろんJavaScriptのサンプルを除いて）

ネットで調べたところ、関数型プログラミングに詳しい人たちが、**関数合成** がこのプログラミングパラダイムにおいて、もっとも重要な概念のひとつだという点である程度意見がまとまっているように思えました。 

このセクションでは、ごく簡単な複数のステップからなる処理を、ふたつのパラダイムで実装して比べてみます。そのパラダイムとは： 命令型プログラミングと関数型プログラミングです。

処理の流れはこんな感じです。 
1. ある整数型を表す文字列を入力として受け取り、整数型の値に変換します。この整数はマイナス記号が付いているかもしれません（ネガティブな値かもしれません）
1. 変換された整数型の値を絶対値に変換します。
1. 絶対値に変換された値に１を足します。

例えば、```"-20"``` を受け取ったとしたら、この一連の処理は、まず```-20```という整数型の値に変換し、絶対値 ```20```に変換し, 最後に```1```を足して ```21```に変換します。

## 命令型プログラミングでやった場合

まずはそれぞれのステップが Scala のライブラリと式でどのように表せるか確認しましょう。

```scala
scala> Integer.parseInt("-20")
scala> Math.abs(-20)
scala> 20 + 1
```

次に、これらのライブラリ関数呼び出しおよび算術式を利用して、新たに関数うを定義しましょう。

```scala
scala> def f(s:String):Int = {
     | val i = Integer.parseInt(s)
     | val a = Math.abs(i)
     | val r = a + 1
     | r
     | }
```

シンタックスは C言語由来のプログラミング言語（Java, C#, JavaScriptなど)でおなじみの雰囲気なので、それらの言語を知ってる人ならすんなりと理解できる実装ですね。

ところで、Scala REPL は、ステートメントが未完の場合は改行の後に自動的にパイプ文字 ```|``` を付けて、続けて記述できるようにしてくれます。ステートメントを完了させるには、記述を完成させたあとEnterキーを打ち続けるとそのうち終わります。

関数の呼び出しもごく普通です。

```scala
 scala> f("-20")    
```

別のやり方として、同じ関数をインラインを多用して1行でやる方法もあります。この場合、ひとつの関数の出力が、直接別の関数の入力になっています。

```scala
scala> def f = (s:String) => Math.abs(Integer.parseInt(s)) + 1
```

ここまでで、Scala を使ってはいますが、関数型プログラミングの特徴を使ってコーディングしているとは言えません。ラムダ式（JavaScriptで言う「アロー関数」）を使っている点でそのようにも見えますが、ラムダ式は無名関数だというだけです。ところで Scala は純粋な関数型プログラミング言語ではありません。関数ファーストな言語であるというだけで命令型言語のように使うことも可能です。（この戦略は```f#```と似ていますし、```Haskell```とは異なります）

## それぞれのステップを関数として見る

関数型プログラミングのやり方をみる前に、それぞれのステップを、```a + 1``` という算術演算も含めて、「独立した関数として扱えること」を確認しておきましょう。これは関数合成をする際に重要になります。

```Integer.parseInt``` の確認の前にまず ```Math.abs``` 関数についてみてみましょう (```Integer.parseInt``` はオーバーロードされている関数なのでちょっとだけ事情が異なります。この次にみていきます。)

```scala
scala> Math.abs _
res46: Int => Int = $$Lambda$1088/0x0000000801835040@5d5db92b
```

このコードで注目したいのがアンダースコア文字 ```_``` が、*この関数が実行されること*防いでいるという点です。代わりに、, instead it will *関数の型を返して*います。アンダースコアを指定しないとScalaのインタープリターが関数の呼び出しを試みて、足りない引数を要求するという事態になってしまうのです。

```Int => Int``` の部分の読み方は *"Int型の引数ひとつを取り、Int型の値を返す関数"*となります。

```=``` 記号の右辺は関数の本体にあたります。この実行例では、僕の実行環境での関数本体の在りかがIDとして表示されていますが、各自の実行環境で値はことなります。

では、```Integer.parseInt``` 関数についてみていきましょう。 

```scala
scala> Integer.parseInt _
               ^
       error: ambiguous reference to overloaded definition,
       both method parseInt in class Integer of type (x$1: String)Int
       and  method parseInt in class Integer of type (x$1: CharSequence, x$2: Int, x$3: Int, x$4: Int)Int
       match expected type ?
```

エラーが出てしまいましたね。コンパイラーは、引数のバリエーションで関数の実装を特定するオーバーロードされた関数について、なんら引数情報を与えていないので、解決できないと文句をいっています。

曖昧さを解決するには、引数の配列を指定します。(今回のケースでは、引数はひとつだけでした).

```scala
scala> Integer.parseInt(_: String)
res47: String => Int = $$Lambda$1089/0x0000000801834840@4fca434c
```

```String => Int``` の部分は *"String型の引数を1つ取り、Int型の値を返す関数"*と読むことが出来ます。

最後に ```a + 1``` という式を見てみましょう。 ```a``` は未知の値なので未知のままにするために、ラムダ式として表します。

```scala
scala> (a: Int) => a + 1
res48: Int => Int = $$Lambda$1090/0x0000000801840840@5d8f3a36
```

ラムダ式は名前の無い関数なので、Scala REPL はその関数型を返すことが出来るのです。

というわけで、３つのステップの全てを*関数*として表せることが確認できました。

このセクションの主な目的は、それぞれのステップを独立した関数として表すことが出来ることを確認することでした。これが意味するのは、それらのステップを他の関数に渡す、もしくは他の関数から返す、異なる関数と交換する、などといった操作が出来るということです。あるプログラミング言語がそのようなことを可能にさせる時、その言語では、*関数はファーストクラスのオブジェクト*だと表現されます。

## 関数型プログラミングでのやり方

では関数型プログラミングを使って同じ関数を実装してみましょう。

ではまず３つの関数に名前をつけることから始めましょう。
これは技術的には必要というわけではないのですが、関数合成のコードを理解する際に、読みやすくするために行います。のちほど、もっと直接的にコーディングした例を示します。

```scala
scala> def s1 = Integer.parseInt(_: String)
scala> def s2 = Math.abs _
scala> def s3 = (a: Int) => a + 1
```

こうすることで３つのステップに```s1```, ```s2```, そして ```s3``` という名前をつけることができました。

次にこの３つの関数を使って ```f2``` という新しい関数を合成します。

```scala
scala> def f2 = s1.andThen(s2).andThen(s3)
```

```andThen``` は Scala の**関数オブジェクト**のメソッドの一つです。下に示すような関係になるように、ひとつの関数の出力を、別の関数の入力にパイプ風に繋げています。

```
s3(s2(s1(x)))
```

一番内側にある関数 ```s1``` が```x```を受け取るところから始まり、その結果をすぐ外側の ```s2``` に渡す。そのように外側に向かって値を渡していく様子が想像できるでしょう。

さらに、```compose```というメソッドを使った他のやり方もあります。下は２つの異なる書式の例ですが、効果は同じです。

```scala
// pattern 1
scala> def f2 = s3.compose(s2).compose(s1)
// patter n2
scala> def f2 = s3 compose s2 compose s1
```

この書式の場合、左から読み進めて３つ目の関数 ```s1``` が、最初の２つ関数(```s3```と```s2```)のペア（合成関数）にどう関わっているかという点で、すこし理解しにくいかもしれません。混乱した場合、以下のように読み解くと理解しやすいでしょう。

まず合成関数 ```s3.compose(s2)``` を見たとき 「どちらの関数が先に呼び出されるだろうか？」と考えてみると、その答えは ```s2``` です。ですから、この合成関数は呼び出される前までは ```s2``` を表していると理解できます。ということは、2つ目の```compose``` は```s2```に対して、あたかも ```s2.compose(s1)``` と記述したような効果を持ちます。よって ```s``` が```s1``` の戻り値を受け取る、```s3(s2(s1(x)))``` のように構成されたことと同じになります。

合成関数 ```f2``` の話に戻りますが、ここで重要なのは ```f2``` *まだ関数の定義にすぎない*ということです。ですから、関数の呼び出しは一切おこなっていません。

ではようやく下に示すように関数を呼び出してみましょう。"-20"引数の値として渡すので、結果は ```21``` が返ってくるはずです。

```scala
scala> f2("-20")
```

前述したように ```s1```, ```s2```, そして ```s3``` はライブラリ関数（および式）の別名でしかなかったので、別名を使わず関数合成すると以下のようになります。

```scala
// 1行でやる
scala> def f3 = (Integer.parseInt(_:String)).andThen(Math.abs _).andThen((x:Int) => x + 1)

// 複数の行にまたがる
scala> def f3 = (Integer.parseInt(_:String)).
     | andThen(Math.abs _).
     | andThen((x:Int) => x + 1)
```

このようにそれぞれのステップを実際の関数と交換するだけです。

# 関数型プログラミングの関数合成を使う利点は？

There are a number of advantages to function composition using functional programming over imperative function defintions.

## 利点１: 変更が簡単で安全

例を見ていきましょう。処理の要求が変わったとして、命令型プログラミングと関数型プログラミングの実装を変更して、それらを比較してみましょう。

既存の処理:

1. ある整数型を表す文字列を入力として受け取り、整数型の値に変換します。この整数はマイナス記号が付いているかもしれません（ネガティブな値かもしれません）
1. 変換された整数型の値を絶対値に変換します。
1. 絶対値に変換された値に１を足します。

ここで、新たに「デバッグ目的として、それぞれのステップごとの途中結果をログに出力する」という要求が発生したとします。

新たな要求を加えた後の処理: 
1. ある整数型を表す文字列を入力として受け取り、整数型の値に変換します。この整数はマイナス記号が付いているかもしれません（ネガティブな値かもしれません）
1. 整数の値をログに出力する
1. 変換された整数型の値を絶対値に変換します。
1. 絶対値に変換された整数の値をログに出力する
1. 絶対値に変換された値に１を足します。
1. １を加えた後の整数の値をログに出力する

注：実際にはI/O処理は"副作用"として考えられるので Haskell のような*純粋な*関数型プログラミング言語では特別な取り扱いをするようです。ここでは理解を助ける目的でI/Oステップを取り入れますが、ベストプラクティスではないというのが私の理解です。

では、命令型プログラミングと関数型プログラミングの実装を比べてみましょう。

### 命令型プログラミングの場合

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

お分かりのように、```println``` を呼び出す命令文を関数 ```f``` の本体の処理の流れに書き加えています。

さらに、新たに加えた命令文が ```i``` や ```a```といった ```r``` ローカル変数を参照しています。今回はたまたま運よく、それらの変数がすでに定義されていましたが、もしローカル変数を使わずにインライン形式で暗黙的に値が関数から関数へ渡されていたなら、出力のためだけに既存のコードをいじる必要があったでしょう。これは既存のコードのレファクタリングを余儀なくさせ、テストの結果を無効にさせ、レグレッションの可能性を生むことになります。

当然ながら、この変更が ```f``` に悪影響を及ぼしていないかを確認するために、単体テスト等を再度実行する必要が生じます。

### 関数型プログラミングの場合

関数型プログラミング言語の関数合成を使って変更をする前に、コンソールに文字列を表示できる新しい関数を定義しなくてはなりません。なぜなら、ビルトインの ```println``` 関数は戻り値を返さないので、関数どうしで数珠つなぎにすることが出来ないからです。

ということで、ごく単純な関数で ```printInt``` をアダプトできるようにしましょう。

```scala
scala> def printInt(x:Int):Int = {
     | println(x)
     | x
     | }
```

```printInt``` という名前のラッパー（wrapper 包み）関数を定義し終えたら、合成された関数のパイプラインに挿入することが出来るようになります。```printInt``` は *reflexive* な関数で、入力した値はそのまま出力されるので、パイプラインのどこに挟み込んでも安全です。

```scala
scala> def f3 = (Integer.parseInt(_:String)).andThen(printInt).andThen(Math.abs _).andThen(printInt).andThen(_ + 1).andThen(printInt)

scala> f3("-20")
-20
20
21
res31: Int = 21
```

同じ関数を、別名を使って、改行も加えて書くと分かりやすいかもしれません。

```scala
scala> def f = s1.andThen(printInt).
     | andThen(s2).andThen(printInt).
     | andThen(s3).andThen(printInt)
```

元々あった関数はそれぞれすでにテストされていて正しいと仮定した場合、ここで必要とされるのは、挿入した関数が"純粋である（ある値に対してかならずいつも同じ値が返され、なおかつ副作用がない）"ということです。

```printInt``` は identity function (日本語では「恒等写像」という難しい言葉なんですね。要は「まったく同じものを返す」という意味)です。副作用に関しては、厳密にはI/Oがあるのでそうとは言えないかもしれませんが、少なくともプログラムデータの状態には干渉しないので、ここは便宜上副作用が無いと考えたとすると、合成された関数は正しく動作すると言えます。

## 利点２: 再利用しやすい

関数合成を意識して定義された関数は、合成関数も含めて再利用がしやすいと言えます。

例えば、まず下のように関数をリストとして保存します。

```scala
scala> val func = List(s2, s3)
```

ここで ```func``` は関数を要素に持つただのリストなので、もっと要素を増やしたりもしくは減らしたり、要素の順番を変えたりと自由に構成できます。

次に、Scala の *高階関数 (higher-order functions)* のひとつを使って合成関数を作ります。 (高階関数は、関数を引きするにとったり、戻り値として返す関数のことです）。

```scala
scala> def fc = func.foldLeft(s1)((ac, cu) => ac.andThen(cu))
scala> fc("-20")
res64: Int = 21
```

```foldLeft``` 関数を使っています。この関数は *初期値* をとります。この場合、String型を引数にとり、Int型を戻り値として返す関数ならなんでもいいです。初期値の関数がリスト内の1番目の関数と繋げられ、その結果がリスト内の2番目（最後）の関数と繋げられます。

最終的に、関数が**鎖のようにリンク**され、関数が合成されます。

さて、このように関数を合成するやり方を使うと、ほかの状況でもその大部分を再利用しやすくなる点をデモしていきます。あらたな要求は、途中結果をログを出力する、でしたから、先ほど定義した ```printInt``` を以下のように合成関数に組み入れることが出来ます。

```scala
scala> val funs = List(printInt _, s2, printInt _, s3, printInt _)
scala> def fc = funs.foldLeft(s1)((c, n) => c.andThen(n))

scala> fc("-20")
-20
20
21
res66: Int = 21
```

途中段階の値がコンソールに出力されているのが分かります。

さらには、```printInt _``` に ```prn``` という別名を与えて仕様することで、もっと読みやすいコードになります。

```scala
scala> def prn = printInt _
scala> def func2 = List(prn, s2, prn, s3, prn)
scala> def fc = func2.foldLeft(s1)((ac, cu) => ac.andThen(cu))
```

## 利点: 抽象・具体の程度を柔軟に指定できる

```foldLeft``` といった高階関数は Scala などの関数型プログラミング言語で特によく利用されます。このタイプの関数は*コレクションの要素に対して処理を行う関数*を引数として受け取り、すべての要素にそれを適用します。関数合成は、この*処理を行う関数*を定義するのに使えます。また、複数の高階関数を合成して別の関数を作ることも出来ます。 
```map```, ```reduce```, ```fold``` などが良く知られた例です。(C# を知っている人なら LINQ の ```Select``` や ```Aggregate``` と言えばピンとくるでしょう）

例えば以下のような２つの、コレクションに対する処理あったとします。

ある文字列型のデータを要素とするコレクションがあったとして (例："-20", "32", "-100")

- 処理A：入力コレクションに対し、それぞれの要素をInt型の絶対値に変換し、それぞれの要素に１を足し、結果のコレクションを返す
- 処理B: 入力コレクションに対し、それぞれの要素をInt型の絶対値に変換し、それぞれの要素に２を掛け、結果のコレクションを返す

両方とも入力と出力は、個別のデータアイテムではなく、データアイテムのコレクションなので、以下のように変換されることになります。

> Stringのコレクション => Intのコレクション => Intのコレクション => Intのコレクション

ところで、処理Aと処理Bは「１を足す」か「２を掛ける」かという演算が異なるだけだという点に注目しましょう。それ以外は、入力はStringのコレクションで、各要素のStringの値はInt型に変換し、そのInt型の値を絶対値に変換し、異なる処理を経て出力はIntのコレクションになる、という点で全く同じです。ですから、プログラミングのベストプラクティスとして、共通する部分はひとつのコードを再利用して冗長をさけたいところです。

さて、これを実装するのに、指定された関数を使って各要素を処理する、```map``` を利用できます。処理Aと処理Bを命令型プログラミングと関数型プログラミング（合成関数機能を利用する）で実装した場合を比較してきます。

### 命令型プログラミングの場合

一般的には、異なる処理部分を含む、名前の付いた２つの関数を定義します。（NOTE: もちろん、共通部分をサブルーチン化したり、オブジェクト指向プログラミングのパターンを使うなどして、冗長化を最小限に抑えるテクニックもありますが、ここでは単純に同じコードを書くやり方にします）

```scala
// 入力データ
scala> def ary = List("-20", "32", "-100")

// 処理Aのための関数
scala> def add1(s:String):Int = {
     | val i = Integer.parseInt(s)
     | val a = Math.abs(i)
     | val r = a + 1
     | r
     | }

// 処理Bのための関数
scala> def mulBy2(s:String):Int = {
     | val i = Integer.parseInt(s)
     | val a = Math.abs(i)
     | val r = a * 2
     | r
     | }

// 処理A
scala> ary.map(add1)
res71: List[Int] = List(21, 33, 101)

// 処理B
scala> ary.map(mulBy2)
res72: List[Int] = List(40, 64, 200)
```

この場合、```add1``` と ```mulBy2```という関数を定義しました。

名前を付けた関数の代わりにラムダ式（無名関数）を使うやり方もあります。その場合は ```add1``` と ```mulBy2``` の代わりにラムダ式を指定します。ただ、ラムダ式を使ったとしても全く同じビジネスロジックをそれぞれの式の中で記述することになります。リファクタリングして同じサブルーチンを呼び出せばいいですが、そうなるとまた名前付きの関数が増える問題に戻ってしまいます。

### 関数型プログラミングの場合

関数合成を使うと、specialize (特殊化）はインラインで行えますし、その実装は柔軟に変更を加えることが出来きます。高階関数に対して直接、合成した関数を渡します。（この点はラムダ式を使う時と原則同じです）

```scala
scala> def ary = List("-20", "32", "-100")

// 処理A
scala> ary.map((Integer.parseInt(_:String)).andThen(Math.abs _).andThen(_ + 1))
res73: List[Int] = List(21, 33, 101)

// 処理B
scala> ary.map((Integer.parseInt(_:String)).andThen(Math.abs _).andThen(_ * 2))
res74: List[Int] = List(40, 64, 200)
```

これ以外にもいくつか特殊化の例を見ていきます。以下のように柔軟に特殊化できるのが関数合成の利点です。

```scala
// 処理の順番を変える
scala> ary.map((Integer.parseInt(_:String)).andThen(_ + 1).andThen(Math.abs _))

// 「１を足す」と「２を掛ける」の両方をする処理
scala> ary.map((Integer.parseInt(_:String)).andThen(Math.abs _).andThen(_ + 1).andThen(_ * 2))

// 共通部分をいったん合成関数にする
scala> def p = (Integer.parseInt(_:String)).andThen(Math.abs _)
p: String => Int

// 共通の関数と特殊化した処理を合わせる
scala> ary.map(p.andThen(_ + 1))
res76: List[Int] = List(21, 33, 101)

scala> ary.map(p.andThen(_ * 2))
res77: List[Int] = List(40, 64, 200)

```

このように、実験が容易になるのが分かるでしょう。開発者は Scala のREPL (Read-Evaluate-Print-Loop) 環境などを利用して生産性を高めることが出来るようになります。

ところで、上の例では「部分適用」というとても強力な関数型プログラミングの機能を使っています。次回は「部分適用」と「カリー化」について解説します。




# Part 2 部分適用とカリー化

部分適用とカリー化は混同されがちだけど、違うものです。部分適用はカリー化とは関係なく出来ますし、カリー化された関数は部分適用を使わずに利用することができます。一緒に使われることが多いようですが、お互いに依存はしていみせん。

混乱を解きほぐすために、まずはカリー化の話題は後回しにして、伝統的な複数の引数を取る関数を使って部分適用をみていきます。次に、カリー化された関数の定義の仕方をおさらいします。その後、*カリー化された関数と部分適用*について解説します。そして最後に*カリー化された関数と合成関数*についてみていきます。

高階関数は部分適用や関数合成とともに利用されることが多いです。その際、カリー化された関数が使わる場合もあるし、無い場合もあるでしょう。高階関数は別に解説する予定です。

## 複数の引数を取る関数を特殊化（specialize）する

部分適用機能を使って、ある関数の引数の（すべてではなく）いくつかを指定することで、新しい関数を定義できます。

この機能の使い方を、まずは１つ以上の引数を取る関数を使ってみていきましょう。

引数を3つ取る、ごく簡単な ```f``` という関数を定義します。

```scala
def f(a: Int, b: Int, x: Int):Int = a * x + b
```

関数の呼び出しはいつもどおり。

```scala
scala> f(2, 5, 3)
res81: Int = 11
```

さてここで、引数 ```a``` の値が ```2``` であるとが、早い段階で分かっていたならば、以下ように出来ます。

```scala
scala> def f2 = f(2, _, _)

scala> f2(5, 3)
res85: Int = 11
```

ここでは、アンダースコア記号は引数 ```b``` と ```x``` への未知の値を表します。対して、引数 ```a``` については値 ```2``` を関数 ```f``` に部分適用して新たな関数を作り出し ```f2``` という名前で定義しました。関数 ```f2``` の呼び出すには引数は２つだけ渡せば済みます。 

さらに別の状況を見てみましょう。もし引数 ```b``` の値だけがすでに分かっているとしたらどうでしょう？ (値は ```5```とします) 

多分、すぐに分かったと思いますが・・・このようになります。

```scala
scala> def f3 = f(_, 5, _)
f3: (Int, Int) => Int

scala> f3(2, 3)
res84: Int = 11
```

```f3``` は２つの引数を取りますが、それは ```a``` と ```x``` に対応します。値 ```5``` はすでに ```b``` の引数として固定されています。

では、 ```a``` と ```b``` の両方の値がすでに分かっていて、未知のものは ```x``` だけだった場合、どうなるかはもうすでにお分かりでしょう。

```scala
scala> def f4 = f(2, 5, _)
f4: Int => Int

scala> f4(3)
res86: Int = 11
```

## カリー化の仕方

まずは、前のセクションで使った関数を*カリー化された状態*にすることから始めます。

ここで改めて確認ですが、カリー化と部分適用はそれぞれ独立した概念であり、一緒に利用される*場合もある*けれど、依存関係にはありません。

Scala では、カリー化された関数は、このように記述できます。

```scala
scala> def fc(a:Int)(b:Int)(x:Int) = a * x + b
fc: (a: Int)(b: Int)(x: Int)Int
```

カリー化された関数 ```fc``` の実際の振る舞いは、前述の複数の引数を取る関数 ```f``` と全く同じです。

すぐに気づく点は、引数の指定の仕方です。```a```, ```b```, そして ```x```それぞれの引数が独立したカッコのペアで囲まれています。こうすることで、関数 ```fc``` は引数を１つだけとり、戻り値として別の関数を返すようになります。

分かりやすいように、複数の引数を取る関数 ```f``` とカリー化された関数 ```fc``` を比較してみましょう。

```scala
scala> f _
res96: (Int, Int, Int) => Int = $$Lambda$1172/0x0000000801894040@ffa7bed

scala> fc _
res97: Int => (Int => (Int => Int)) = $$Lambda$1173/0x0000000801893840@4e843bf6
```

複数の引数をとる関数の方はよく見慣れた感じです。 ```(Int, Int, Int) => Int``` の部分は *「この関数はInt型の引数を３つ取り、Int型の戻り値を返す」* を意味します。

カリー化された関数（引数を1つしかとらない方) は様子が随分と異なります。 ```Int => (Int => (Int => Int))``` の部分は *「この関数はInt型の引数を１つ取り、戻り値として```(Int => (Int => Int))```型の関数を返す」* という意味になります。

実は、このブログ記事はもともと英語で書いたんですが、このカリー化された関数の型を表すにい日本語は大変不向きです。英語で表現するならもっと自然です。

> *it is a function that takes a single argument of Int and returns a function that takes a single argument of Int type and returns a function that takes a single argument of Int type and returns a value of Int type."*

（読み方のコツは、```=>``` を見つけたら ```=>``` の左側について「a function that takes... and returns (a function that...)」と始めることです。英語だとうまく入れ子の状態とマッチして文に出来ます。）

ここで鍵になる考え方は 「・・・引数を１つ取り・・・」という部分です（英語の"...takes a single argument..."の部分。英語では引数の数と同じだけ、"...takes a single argument..." という表現が出現しているのが分かると思います（日本語だと難しい・・・）。

ということで、関数 ```fc``` は引数を１つ求めているので、ある値を渡したとすると、戻り値として```(Int => (Int => Int))```という型の関数を返します。

カリー化された関数では、２つ以上の引数を一度に渡すことはありませんし、それは入れ子状態の関数のどのレベルでも同じです。（2つ以上の値を「組（tuple)」として渡す場合はあるかもしれませんが、それでも引数としては1つです）

#### 寄り道：JavaScriptでカリー化された関数を見てみよう

JavaScript で同じ関数をカリー化して利用することも出来ます (この例は Google Chrome の開発者向けツールのコンソールに直接入力しました).

```javascript
> var fc = a => b => x => a * x + b
< undefined
> fc
< a => b => x => a * x + b
```

もしかしたら、伝統的な ```function``` 予約語を使って定義したバージョンの方が理解しやすいかもしれません。

```javascript
var fc3 = function(a) { 
    return function(b) { 
        return function(x) { 
            return a * x + b 
            } 
        } 
    }
```

ところで、引数 ```a``` の値が、最内部の関数の本体からもアクセスできるのはクロージャー（closure) のおかげです。引数 ```b``` に関しても同じです。そうやって式 ```a * x + b``` に ```a``` と ```b``` の値が渡されます。クロージャは JavaScript でカリー化を可能にさせる機能なのです。

２つの関数 ```fc``` と ```fc3``` は振る舞いとしては全く同じですし、両方ともカリー化されているので、呼び出し方法も同じです。（記述の仕方としては ```fc``` の方がずっと簡単ですね）

```javascript
fc(2)(5)(3)
11
fc3(2)(5)(3)
11
```

カリー化された関数は引数を１つしかとらないので、それぞれの入れ子になった関数に対しても同様に引数は１つしか渡しません。そのため ```(2)```, ```(3)```, そして最後に ```(3)``` という形で引数を別々に渡しています。

では、JavaScript の世界を離れて、Scala の世界に戻りましょう。

Scala でのカリー化された関数の呼び出しもこのようになります。

```scala
scala> fc(2)(5)(3)
res99: Int = 11
```

特に驚きの要素はありませんね。記述の仕方としては、引数として１つ渡すというのは変わらないので、JavaScriptの場合と全く同じです。

#### 複数の引数を取る関数をカリー化する

すでに存在する複数の引数を取る関数を、簡単にカリー化できるとしたら便利ですよね。幸運にも Scala にはビルトインで簡単にカリー化できる機能があります。

以下では、３つの引数を取る関数 ```f``` のカリー化されたバージョンを ```fc``` という名前を付けて定義しています。

```scala
scala> def fc = (f _).curried
fc: Int => (Int => (Int => Int))

scala> fc(2)(5)(3)
res103: Int = 11
```

```(f _)``` の部分は関数 ```f``` そのものを表していて、関数を呼び出さないようにしています。アンダースコアを指定することでインタープリターが関数呼び出しを実行することを防いでいます。```curried``` というメンバーからカリー化された関数を得ることが出来ます。

## カリー化された関数から特殊化(specialized)された関数を作る

ｋの段階は、カリー化された関数からどのように特殊化された関数を作るか、という話題はもう必要ないでしょう。もうすでに何度も例を見てきましたから。

以下では、引数```a``` と ```b``` に値を渡し（ただし```x```に関しては未知のままとする）、新たに ```fc4``` という名前の特殊化された関数を作ります。 関数```fc4``` を呼び出す際は、引数 ```x``` に対する値を渡すだけです。

```scala
scala> def fc4 = fc(2)(5)
fc4: Int => Int

scala> fc4(3)
res104: Int = 11
```

## カリー化された関数で関数合成をする

### ```andThen``` メソッドと```compose``` メソッドのおさらい

すでに ```andThen``` メソッドを使って関数を合成するやり方を見てきましたが、```compose``` メソッドを使って同じように関数を合成できます。このセクションでの例は```compose``` メソッドを利用するので、その使い方をまず確認しておきましょう。

以下のような２つの関数があったとします。

```scala
scala> def inc(x:Int) = x + 1
scala> def mulBy2(x:Int) = x * 2
```

この二つの関数を ```mulBy2(inc(x))``` ような関係にしたいとします。例えば、 ```x=1``` の時 ```4``` が結果になるように、または ```x=2``` の時 ```6``` が結果になるような組み合わせです。

```compose``` メソッドを使うと、このように関数を合成できます 

```scala
scala> def c1 = (mulBy2 _).compose(inc _)
```

関数 ```c1``` を呼び出して、正しく合成されているか確認します。

```scala
scala> c1(1)
res206: Int = 4

scala> c1(2)
res207: Int = 6
```

同様に逆の関係 ```inc(mulBy2(x))``` にするには以下のように組み合わせます（この場合 ```x=1``` なら結果は ```3``` で、```x=2``` なら結果は ```5``` になるはずです）

```scala
scala> def c1 = (inc _).compose(mulBy2 _)
```
つまりコンポーズする関数は*コンポーズされた関数の出力*をその入力にとるわけです。

以下、```compose``` メソッドを利用して、先に定義したカリー化された関数 ```fc``` （下参照）を改造していきます。

```scala
def fc(a:Int)(b:Int)(x:Int) = a * x + b
```

ではちょっとシナリオを設定しましょう。この関数 ```fc```は引数を3つとるわけですが、 ```a``` と ```b``` 関して、以下のようなパラメータ検証をすることになったと仮定します。

- 引数```a``` の値は常に絶対値に変換してのち、```fc``` に渡さなくはならない (ヒント：Math.abs ライブラリ関数が使えそうです)
- 引数```b``` の値は5以下でなくてはならない (ヒント: Math.min ライブラリ関数が使えそうです)

これらを式として表すとこうなります：

```scala
Math.abs _
def atMost5 = Math.min(5, (_:Int)) // ヘルパー関数を作成しました
```

ヘルパー関数 ```atMost5``` がきちんと動くかテストしておきましょう（高階関数 ```map``` はこんな時にも便利です）

```scala
scala> (1 to 10) map(atMost5)
res8: IndexedSeq[Int] = Vector(1, 2, 3, 4, 5, 5, 5, 5, 5, 5)
```

入力が５より大きい時でも出力は５になっています。

では、引数 ```a``` と ```b``` の値それぞれに事前処理を施しつつ、関数 ```fc```と合成してみましょう。まずは引数 ```a``` から初めて、段階を追ってみていきます。

```scala
scala> (fc _).compose(Math.abs _)
res10: Int => (Int => (Int => Int)) = // 右辺は省略しました
```

この合成のパターンは ```fc(Math.abs(a))``` という関係に２つの関数を結び付けます。ここで気を付けなくてはいけないのは、この合成された関数はあくまでも *まだ３つの引数を取る関数* であるということです。決して *２つの引数を取る関数*にはなっていません - それは部分適用の話です。これは関数合成の話であって、どの引数にもいまだ値は適用されていません。重要な点は、後の実行時に、引数```a``` として渡される値は、合成された関数 ```Math.abs``` を*通って* ```fc``` に到達する、ということです。

次に、２つ目の引数 ```b``` に対する事前処理（最大で値は```5```になるように強制する）を行うように関数を合成しましょう。この場合、```b``` へ値がヘルパー関数 ```atMost5``` を*通り抜けていく* ように構成しなければなりません。つまり、１つ目の引数をなんとかスキップしなくてはならないのです。

```scala
scala> (fc _).compose(Math.abs _)(_:Int).compose(atMost5)
```

注目してもらいたいのは ```(fc _).compose(Math.abs _)``` の部分のすぐあとに来る ```(_:Int)``` の部分です。これは１つめの引数は「未知のまま」としてスキップしていて、２つ目の引数に対して - 言い換えると「２つ目の引数を受け取る、１つしか引数を受け取らないカレー化された関数」に対して - ```compose``` メソッドを使って ```atMost5``` と結び付けているのです。

では、この合成された関数に名前をつけて新し関数として定義しましょう。

```scala
scala> def fcx = (fc _).compose(Math.abs _)(_:Int).compose(atMost5)
fcx: Int => (Int => (Int => Int))
```

合成関数 ```fcx``` はいまだにトータルで引数を３つ取ることは変わりません（```=>``` 記号の左側が引数ですから、数えるとInt が３つあります）。まだ引数の値はひとつも渡していないので、ベースの関数 ```fc``` は引数を３つ必要とします。 (ヘルパー関数 ```atMost5``` に対して値```5```を部分適用して固定しましたが、これは ```fc``` 関数に対する部分適用ではありません)。

ベースになったカリー化された関数 ```fc``` を一切変更することなく、２つの引数が事前処理されるように構成することができました。

同様のことを命令型プログラミングでやるとどのようになるでしょう。```fc``` 関数本体を変更するやり方もあるでしょうし、呼び出しの際に、インラインで事前処理の関数を呼び出す方法もあるでしょうし、一種の関数合成として別名を与えることも出来ます。ただ、はたしてそれが好ましい方法なのかどうかは議論の余地があるでしょう。

関数型プログラミング、さらにはカリー化と合成関数は、このような特殊化(specialization)が普通に起こるという状況で、その柔軟性を発揮することが出来ます。

では実際にテストしてみましょう。テストケースは３つ用意します。

- ```fcx(2)(5)(1)``` は、 ```2 * 1 + 5``` なので結果は ```7```になる。
- ```fcx(-2)(5)(1)``` は、まず ```-2``` が ```Math.abs``` によって ```2``` に変換され、```2 * 1 + 5``` となるので、結果は ```7```になる。
- ```fcx(2)(8)(1)``` は、まず ```b=8``` が ```atMost5``` によって強制的に ```b=5``` になり、```2 * 1 + 5```となるので、結果は ```7```になる。 

テスト結果：

```scala
scala> fcx(2)(5)(1)  
res27: Int = 7       
                     
scala> fcx(-2)(5)(1) 
res28: Int = 7       
                     
scala> fcx(-2)(8)(1) 
res29: Int = 7       
```

期待通りの結果になりました。

## カリー化された関数の利点とは？

オンラインでこの答えを探すと（少なくとも英語圏では）あまり統一の取れた見解はでてきませんでした（もちろん、調査する私の力不足も含めての結果ですが・・・）。多く見られるのが、部分適用の利点をカリー化の利点としてしまっている主張です。ですが、この記事でも解説した通り、部分適用はカリー化を必要しないので、部分適用の効果をカリー化の効果とするのは正しくありません。

ですが、カリー化（とカリー化された関数）の利点を、部分適用とは別けて解説しているブログをとりあえずひとつ見つけました。 [article by Iven Marquardt](https://medium.com/@iquardt/currying-the-underestimated-concept-in-javascript-c95d9a823fc6)。この記事によると、カリー化の利点は 

> *abstracts the variations of number of arguments* （引数の数の違い・バリエーションを抽象化する）

とあります。これはなるほどと納得できるものです。関数を合成しようとして、よく遭遇する「壁」として、引数と戻り値の型の他に、引数の数があります。うまく合成してパイプライン、またはツリー構造に構成したくとも、既存の関数が複数の引数を取るためにうまくいかなかったりします。ですが、カリー化は「引数は常にひとつ」という状態を保証するので、粒がそろうことで関数合成がしやすくなります。

関数型プログラミング入門者の私にとって、もっとも納得いく説明はこのようなものでした。


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

