[![Build and Release Docker Image](https://github.com/tmunongo/tinyutils/actions/workflows/release.yml/badge.svg)](https://github.com/tmunongo/tinyutils/actions/workflows/release.yml)

# tinyutils

A self-hostable collection of privacy-first web utilities built with ASP.NET Core and Blazor.

It's mostly a collection of tools that I often find myself quickly googling, but oftentimes it's never clear whether or not your that JSON response with API keys, etc. will end up in the [wrong hands](https://cyberpress.org/developers-leak-passwords-and-api-keys/).

# Why ASP.NET

A few reasons:
1. I've been slowly getting into game dev and this was the perfect opportunity to improve my C# in a familiar context
2. I wanted a compiled langauge for *performance*, especially for the upcoming video features, and I've heard good things about C#.
3. I have always liked the idea of using a single language to build full-stack web applications. I have tried Laravel and Go. Maybe 3rd time's the one. Plus, C# is also useful beyond just the web, and I can't say the same for Ruby.

## Features
-  **Privacy First**: All processing happens on your server
- ✅ **JSON Formatter**: Validate and beautify JSON
- [🕐] **Image Tools**: Convert, compress, and process images
- [] **PDF Tools**: Convert PDFs to images
- [] **Video Tools**: Create GIFs from video clips
- [] **Text Utils**: Base64, slugify, UUID, hashing

# Development

```bash
# Install dependencies
dotnet restore

# Run in watch mode
dotnet watch run

# Run tests
dotnet test
```