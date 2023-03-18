# In Memory Cache Example

My backend needs to collect data. This takes a lot of time, so I don’t want to collect the data with every API call, but instead cache it in memory. The cache should be updated regularly in a background thread. However, it should also be possible to explicitly initiate the update via an API call. How could this be implemented in .NET Core?

This example shows how this could be solved.

You can find further explanations in my blog post [Cache data in memory and update it regularly via a background service (.NET Core MinimalAPI example)](https://frank.woopec.net/2023/03/18/background-service-in-mem-cache.html) 

### Testing

For testing you can open a command line and use `curl`

```
curl -s https://localhost:59999/summary/authors
curl -d "" https://localhost:59999/triggerCacheUpdate/authors
```

The port `59999` must be replaced with the port that Visual Studio entered in file `.\InMemCacheMinimalApi\Properties\launchSettings.json` after the first startup on your machine. 