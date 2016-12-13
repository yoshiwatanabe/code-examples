open System

let victory() =
  for frequency in [100 .. 50 .. 2000] do
    Console.Beep(frequency, 25)

// did not work. don't understand this yet
