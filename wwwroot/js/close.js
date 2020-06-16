function openForm() {
  document.getElementById("myForm").style.display = "block";
}

function closeForm() {
  document.getElementById("myForm").style.display = "none";
}

function countChar(val) {
    var len = val.value.length;
    if (len >= 233) {
      val.value = val.value.substring(0, 233);
    } else {
      $('#charNum').text(233 - len + "/233");
    }
  };


