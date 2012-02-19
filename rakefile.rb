require 'albacore' # 'gem install albacore' if you haven't done that yet!

build_configuration = "Debug"

msbuild :build do |msb|
	msb.solution = "TopDevLinks.sln"
	msb.targets :clean, :build
	msb.properties :configuration => build_configuration
end

nunit :test => :build do |nunit|
	nunit.command = "packages\\NUnit.2.5.10.11092\\tools\\nunit-console.exe"
	nunit.assemblies "TopDevLinks.Tests\\bin\\debug\\TopDevLinks.Tests.dll"
end

task :default => [:test]
