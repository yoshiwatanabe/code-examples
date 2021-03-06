let daysOfTheYear =
  seq {
    let months =
      [
        "Jan"; "Feb"; "Mar"
      ]

    let datsInMonth month =
      match month with
      | "Feb"
          -> 28
      | "Apr" | "Jun" | "Sep" | "Nov"
          -> 30
      | _ -> 31

    for month in months do
      for day = 1 to daysInMonth month do
        yield (month, day)
  };;
