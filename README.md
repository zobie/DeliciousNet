Delicious.Net
============

Delicious.Net is an API written in C# using the .NET 2.0 framework which allows developers to interact with the [del.icio.us](http://del.icio.us) social bookmarking service. This framework was developed for use in the del.icio.us client [Netlicious](http://www.procanta.com), but is available for everyone to use and enjoy under the BSD License. Please take the time to report bugs so that they can be fixed.

Three objects are defined that can be used to interact with del.icio.us:

    Delicious.Post
    Delicious.Tag
    Delicious.Bundle

Before connecting to the del.icio.us servers you must set a username and password: 

    Delicious.Connection.Username = "SomeUser";
    Delicious.Connection.Password = "MyPassword";

To retrieve all posts and iterate through the list: 

    List<Delicious.Post> posts = Delicious.Post.Get();
    foreach (Delicious.Post post in posts)
    { ... }

To add a new post:

    Delicious.Post.Add (url, description, extended, tags, date, replace, shared);

To delete a post: 

    Delicious.Post.Delete (url);

