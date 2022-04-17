open System
open System.IO
open System.Globalization
open System.Text.RegularExpressions
open System.Collections.Generic

let comparer = Comparer<string>.Default
let filepath = __SOURCE_DIRECTORY__ + @"/ShortStory.txt"
let lines = File.ReadAllLines(filepath) |> Seq.toList
let mutable formatted = []

// Loop over and format strings.
for value in lines do

    formatted <-
        [ Regex.Split(value, @"\042|\. |.\042|\?\042|\? |\! ") ]
        |> List.append formatted
//        [ Regex.Split(value, @"\. |.\042|\?\042|\? |\! ") ]


let final_sorted =
    Seq.concat formatted
    |> Seq.map (fun line -> line.TrimStart(' '))
    |> Seq.sortWith (fun x y -> comparer.Compare(x, y)) //default sort implies "tv" *is* smaller than "TV", so used CompareTo
    |> Seq.filter (String.IsNullOrWhiteSpace >> not)


File.WriteAllLines(__SOURCE_DIRECTORY__ + @"/sorted_result.txt", final_sorted)
