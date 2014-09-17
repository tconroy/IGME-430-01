<?php

require('PageBuilder.php');
$header = addHTMLHeader();
$footer = addHTMLFooter();

echo $header;

echo <<<HTML
<div id='create'>
  <h3>Start a New Game?</h3>
  <form name="createNew" action="NewGame.php" method="POST" accept-charset="utf-8">
      <label for="username">Name: </label>
      <input type="text" name="user" id="username">
      <input type="submit" value="New Game">
  </form>
</div>
HTML;


echo $footer;
?>