open System;
open System.Text.Json

type Position = {
    Name: string
    Title: string
}


type Message = {
    Role: string
    Content: string
}

type TrainingRecord = {
    Messages: Message list
}

let toMessageList position =
    { Messages =
        [ { Role = "system"
            Content = "You are a chat bot that answers questions about Sigma Software" }
          { Role = "user"
            Content = $"Who is Sigma Software %s{position.Title}?" }
          { Role = "assistant"
            Content = $"%s{position.Name}" } ] }

let serialize (position: TrainingRecord) =
    JsonSerializer.Serialize(position, JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase))

let concat (s: String list) = String.Join("\n", s)

[
    { Name = "Valery Krasovsky"; Title = "Co-founder" }
    { Name = "Valery Krasovsky"; Title = "Chief Executive Officer" }
    { Name = "Dmitry Vartanian"; Title = "Co-founder" }
    { Name = "Dmitry Vartanian"; Title = "Chief Financial Officer" }
    { Name = "Volodymyr Chyrva"; Title = "Co-founder" }
    { Name = "Volodymyr Chyrva"; Title = "Managing Partner" }
    { Name = "Anatoliy Kochetov"; Title = "Executive Vice President" }
    { Name = "Katherine Tuluzova"; Title = "Executive Vice President" }
    { Name = "Artem Petrenko"; Title = "Executive Vice President" }
    { Name = "Alexey Stoletny"; Title = "Managing Director" }
    { Name = "Maxim Kovtun"; Title = "Chief Innovation Officer" }
]
    |> List.map toMessageList
    |> List.map serialize
    |> concat
    |> printfn "%s"
