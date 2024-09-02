function getallCategory() {
  let url = "https://localhost:7216/api/Categories";
  var element = document.getElementById("Category");

  fetch(url)
      .then(response => response.json())
      .then(data => {
          console.log(data); // Log the data to check the number of items returned

          data.forEach(category => {
              let card = document.createElement('div');
              card.className = 'card';
              card.style.width = '18rem';

              card.innerHTML = `
                  <img src="${category.categoryImage}" class="card-img-top" alt="${category.categoryName}">
                  <div class="card-body">
                      <h5 class="card-title">${category.categoryName}</h5>
                      <h6 class="card-subtitle mb-2 text-body-secondary">Card subtitle</h6>
                      <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                      <a href="CategoryDetails.html" class="card-link">Card link</a>
                      <a href="#" class="card-link">Another link</a>
                  </div>
              `;

              let cardLink = card.querySelector('.card-link');
              cardLink.addEventListener('click', function() {
                  localStorage.setItem("id", category.categoryId);
              });

              element.appendChild(card);
          });
      })
      .catch(error => console.error('Error fetching categories:', error));
}

getallCategory();