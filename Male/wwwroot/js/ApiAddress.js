const provinceSelect = document.querySelector('.province');

if (provinceSelect) {
    fetch('https://provinces.open-api.vn/api/?depth=2')
        .then((res) => res.json())
        .then((data) => {
            console.log(data);
            [...data].forEach((pr) => {
                const option = document.createElement('option');
                option.value = pr.name;
                option.textContent = pr.name;
                option.setAttribute('codeId', pr.code);
                provinceSelect.appendChild(option);
            });

            provinceSelect.addEventListener('change', (e) => {
                const districtsSelect = document.querySelector('.districts');
                if (!districtsSelect) return;
                districtsSelect.innerHTML = '';
                var select = e.target;

                var selectedOption = select.options[select.selectedIndex];
                const codeId = selectedOption.getAttribute('codeId');
                const province = [...data].find(
                    (x) => x.code.toString() === codeId,
                );

                const { districts } = province;
                console.log(districts);
                [...districts].forEach((d) => {
                    const option = document.createElement('option');
                    option.value = d.name;
                    option.textContent = d.name;
                    districtsSelect.appendChild(option);
                });
            });
        });
}
