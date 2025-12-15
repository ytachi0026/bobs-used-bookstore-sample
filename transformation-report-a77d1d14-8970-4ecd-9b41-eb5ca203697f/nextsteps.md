# Next Steps

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies a cross-platform .NET version (net6.0, net7.0, or net8.0) rather than .NET Framework versions.

### 2. Run Unit Tests

Execute the test suite to ensure functionality remains intact:

```bash
cd app/Bookstore.Domain.Tests
dotnet test --verbosity normal
```

Review test results for any failures or warnings that may indicate compatibility issues.

### 3. Check Package Dependencies

Verify that all NuGet packages are compatible with the target framework:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated or deprecated packages to their latest stable versions that support cross-platform .NET.

### 4. Validate Configuration Files

Review configuration files for platform-specific settings:

- Check `appsettings.json` and `appsettings.Development.json` in Bookstore.Web
- Verify connection strings are using cross-platform compatible formats
- Ensure file paths use `Path.Combine()` rather than hardcoded separators

### 5. Test Data Access Layer

Validate the Bookstore.Data project functionality:

- Verify database provider compatibility (Entity Framework Core versions)
- Test database migrations if applicable:

```bash
cd app/Bookstore.Data
dotnet ef migrations list
```

- Confirm connection to the database from the new runtime

### 6. Run the Web Application Locally

Start the Bookstore.Web application to verify runtime behavior:

```bash
cd app/Bookstore.Web
dotnet run
```

Test the following:

- Application starts without runtime errors
- All endpoints respond correctly
- Static files are served properly
- Authentication and authorization function as expected

### 7. Cross-Platform Testing

If possible, test the application on different operating systems:

- Build and run on Linux: `dotnet build && dotnet run`
- Build and run on macOS: `dotnet build && dotnet run`
- Verify Windows compatibility remains intact

### 8. Review CDK Infrastructure Code

Examine the Bookstore.Cdk project for AWS CDK compatibility:

```bash
cd app/Bookstore.Cdk
dotnet build
```

- Verify CDK constructs are compatible with the new .NET version
- Test CDK synthesis: `cdk synth` (if CDK CLI is installed)
- Review generated CloudFormation templates for accuracy

### 9. Performance Baseline

Establish performance metrics for the migrated application:

- Measure application startup time
- Test response times for key endpoints
- Monitor memory usage during typical operations
- Compare against legacy application metrics if available

### 10. Code Analysis

Run static code analysis to identify potential issues:

```bash
dotnet build /p:EnableNETAnalyzers=true /p:AnalysisLevel=latest
```

Address any warnings or suggestions related to cross-platform compatibility.

## Deployment Preparation

### 1. Update Deployment Scripts

Review and update any deployment scripts or documentation:

- Modify scripts to use `dotnet publish` instead of MSBuild
- Update runtime identifiers if creating self-contained deployments
- Verify publish profiles in Bookstore.Web

### 2. Publish the Application

Create a release build to validate the publish process:

```bash
cd app/Bookstore.Web
dotnet publish -c Release -o ./publish
```

Verify the published output contains all necessary files and dependencies.

### 3. Environment Configuration

Ensure environment-specific configurations are properly set:

- Validate environment variables are correctly referenced
- Test configuration overrides for different environments
- Verify secrets management approach is compatible

### 4. Dependency Verification

Confirm all runtime dependencies are included:

```bash
cd app/Bookstore.Web/publish
dotnet Bookstore.Web.dll
```

Verify the application runs from the published directory without requiring the SDK.

### 5. AWS Deployment Validation

If deploying to AWS:

- Test the CDK deployment in a non-production environment
- Verify IAM roles and permissions are correctly configured
- Confirm the runtime environment in AWS supports the target .NET version

## Documentation Updates

- Update README files with new build and run instructions
- Document any breaking changes or behavioral differences
- Update developer setup guides to reference .NET SDK instead of .NET Framework
- Revise deployment documentation with new publish procedures

## Final Checklist

- [ ] All projects build successfully
- [ ] Unit tests pass
- [ ] Application runs locally without errors
- [ ] Database connectivity verified
- [ ] Configuration files reviewed
- [ ] Cross-platform compatibility tested
- [ ] Performance is acceptable
- [ ] Deployment process validated
- [ ] Documentation updated