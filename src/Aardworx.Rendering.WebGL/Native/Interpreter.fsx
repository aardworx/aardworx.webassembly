open System.IO



type ArgumentType =
    | Int
    | Float
    

let rec allCombinations (l : list<list<'a>>) =
    match l with
    | [] -> [[]]
    | h :: t ->
        let rest = allCombinations t
        h |> List.collect (fun h ->
            rest |> List.map (fun r ->
                h :: r
            )
        )
        

let mask (args : list<ArgumentType * bool>) =
    let mutable mask = 1
    for t in args do
        let value =
            match t with
            | Int, false -> 0
            | Float, false -> 0
            | Int, true -> 1
            | Float, true -> 1
            

        mask <- (mask <<< 1) ||| value

    mask


let b = System.Text.StringBuilder()
let printfn fmt = Printf.kprintf (fun str -> b.AppendLine str |> ignore) fmt

printfn "#include <emscripten.h>"
printfn "#include <emscripten/html5.h>"
printfn "#include <string.h>"
printfn "#include <stdlib.h>"
printfn "#include <stdint.h>"
printfn ""

printfn "int emInterpret(void* instructions, int length) {"
printfn "   intptr_t code = (intptr_t)instructions;"
printfn "   intptr_t e = code + length;"
printfn "   while(code < e) {"
printfn "        int16_t op = *(int16_t*)code;"
printfn "        code+=2;"
printfn "        void* f = *(void**)code;"
printfn "        code += sizeof(void*);"
printfn "        switch(op) {"
let mutable total = 0
for c in 0 .. 8 do
    let vs = [Int, false; Int, true] 
    let alt = List.init c (fun _ -> vs) |> allCombinations
    for pars in alt do
        let m = mask pars
        let argTypes = 
            pars |> List.mapi (fun i (a,_) ->
                match a with
                | Int -> "int"
                | Float -> "float"
            )
           

        printfn "        case 0x%04X:" m
        let mutable offset = 0
        let reads =
            pars |> List.map (fun (t, p) ->
                let t =
                    match t with
                    | Int -> "int"
                    | Float -> "float"
                let access =
                    if p then
                        if offset = 0 then sprintf "**(%s**)code" t
                        else sprintf "**(%s**)(code + %d)" t offset
                    else
                        if offset = 0 then sprintf "*(%s*)code" t
                        else sprintf "*(%s*)(code + %d)" t offset

                offset <- offset + 4

                access
            )

            //printfn "            %s arg%d = *(%s*)code;" t i t
            //printfn "            code += sizeof(%s);" t

        let uses = reads |> String.concat ", " //types |> List.indexed |> List.map (fun (i,_) -> sprintf "arg%d" i) |> String.concat ", "

        printfn "            ((void (*) (%s))f)(%s);" (String.concat ", " argTypes) uses
        printfn "            code += %d;" offset
        printfn "            break;"
        total <- total + 1

printfn "        default:"
printfn "            return -1;"
printfn "        }"
printfn "    }"


printfn "    return 0;"
printfn "}"

System.Console.WriteLine("total: {0}", total)



File.WriteAllText(Path.Combine(__SOURCE_DIRECTORY__, "Interpreter.c"), b.ToString())






