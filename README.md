<h1>Slacky</h1>
<h2>A workplace collaboration platform made with Asp.Net </h2>
<img src='https://github.com/w-i-l/ReadME.md-template/assets/65015373/89ecaf19-68ad-42b5-9f4a-6c1bf3c7fafc'/>



<br>
<hr>
<h2>About it</h2>
<p>This project tried to recreate the functionality of the Slack application. Its capabilities are restricted to only one company.</p>

<p>It offers the possibility of creating an account with an email and password, adding and joining channels, internal moderation inside a channel and allowing a single person to take care of the entire application, having an admin role. Besides sending messages, users can share images and embeded links in the channel.</p>

<p>The application features a presentation homepage for those who are not logged in, that presents the main functionality of the project.</p>

<br>
<hr>
<h2>How to use it</h2>
<p>All the data and packages are included in the bundle, so you just have to open it in a specific environment that has <code><a href="https://dotnet.microsoft.com/en-us/download">.Net</a> framework</code> installed. For the development,
we used <a href="https://visualstudio.microsoft.com/">Visual Studio</a>.</p>



<br>
<hr>
<h2>How it works</h2>
<p>We divide the responsibilities and capabilities of the users into three main categories, sorted descending by their influence:</p>
<ol>
    <li><code>Admin</code>
        <ul>
            <li>Is the only one who can manage the categories, performing CRUD operations</li>
            <li>Is automatically assigned a <code>moderator</code> on every channel</li>
            <li>He is immune to any destructive operation, such as being removed from a channel.</li>
        </ul>
    </li>
    <li><code>Moderator</code>
        <ul>
            <li>Can invite and kick out users from a specific channel, where he moderates</li>
            <li>Promote and demote other users from the specific channel</li>
            <li>Edit and delete others messages in the channel</li>
        </ul>
    </li>
    <li><code>User</code>
        <ul>
            <li>He can search and join channels</li>
            <li>Create a channel, and automatically will become its <code>moderator</code>.</li>
            <li>Send, delete and edit his messages</li>
        </ul>
    </li>
</ol>

<br/>
<p>Based on his role, the user can perform the actions described above.</p>
<p>The <code>admin</code> should create categories in order for <code>users</code> to be able to add channels. After creating a channel, the users can freely use it as long as they are not kicked out.</p>

<br>
<hr>
<h2>Tech specs</h2>
<p>Database diagram:</p>
<img src="https://github.com/w-i-l/ReadME.md-template/assets/65015373/699d3d9a-1c89-48cc-a34d-8ef66ecdb56a"/>

<br/>
<p>For showing all the channels for a user we used a <code>middleware</code> that takes all the user channels and lists in the left bar.</p>

<p>The authentification system is built using <code>Asp.Net Identity framework</code>.</p>


<br>
<hr>
<h2>Roles</h2>
<p>This project was done in a collaborative manner, having online meettings once every 1-2 weeks and organizing our tasks using <a href="https://trello.com/">Trello</a>.</p>
<p>Below are listed the main tasks done:</p>
<ul>
    <li><a href="https://github.com/w-i-l">Ocnaru Mihai</a>
        <ul>
            <li>Users and roles</li>
            <li>Homepage for unlogged users</li>
            <li>Channel class</li>
            <li>Search bar</li>
            <li>Show all channels + middleware</li>
            <Li>Connected messages - categories + channels</Li>
            <li>Channel moderator</li>
        </ul>
    </li>
    <li><a href="https://github.com/Epure-Tofanel-Carlo">Epure Carlo</a>
        <ul>
            <li>Category class</li>
            <li>Message class</li>
            <li>Database design + models</li>
            <li>Multimedia messages</li>
            <li>Controllers restirctions</li>
            <li>Admin actions</li>
            <li>Documentation for presentation</li>
        </ul>
    </li>
</ul>
