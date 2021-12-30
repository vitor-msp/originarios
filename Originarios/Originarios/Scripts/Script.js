/********************* Formatação máscara data nascimento *********************/
const formatDtNasc = () => {
    const dtNasc = document.getElementById('dtNasc').innerHTML.trim();
    if (dtNasc) {
        document.getElementById('dtNasc').innerHTML = dtNasc.substring(0, 10);
        return;
    }
    const hiddenDtNasc = document.getElementById('hiddenDtNasc').value.trim();
    if (hiddenDtNasc) {
        const day = hiddenDtNasc.substring(0, 2);
        const month = hiddenDtNasc.substring(3, 5);
        const year = hiddenDtNasc.substring(6, 10);
        document.getElementById('dtNasc').value = `${year}-${month}-${day}`;
    }
}
window.addEventListener('load', formatDtNasc);

/********************* Códigos para cortar imagem *********************/
///////////////////// códigos gerais /////////////////////
let cropper1 = null;
let preCrop1 = document.getElementById('preCrop1');
let cropper2 = null;
let preCrop2 = document.getElementById('preCrop2');
let cropper3 = null;
let preCrop3 = document.getElementById('preCrop3');
let cropper4 = null;
let preCrop4 = document.getElementById('preCrop4');
const initCrops = () => {
    initCrop1();
    initCrop2();
    initCrop3();
    initCrop4();
};
window.onload = initCrops;

///////////////////// cropper imagem 1 /////////////////////
const initCrop1 = () => {
    'use strict';
    const options = {
        aspectRatio: 3 / 5,
        preview: '.posCrop1'
    };
    cropper1 = new Cropper(preCrop1, options);
};
const carregarImg1 = (event) => {
    cropper1.destroy();
    cropper1 = null;
    let reader = new FileReader();
    reader.onload = () => {
        preCrop1.src = reader.result;
        initCrop1();
    }
    reader.readAsDataURL(event.target.files[0]);
};
const obterCanvas1 = () => {
    const canvas = cropper1.getCroppedCanvas({ maxWidth: 300, maxHeight: 500 });
    const urlcanvas = canvas.toDataURL('image/png');
    document.getElementById('hiddenCrop1').value = urlcanvas;
}
document.getElementById('btnCanvas1').addEventListener('click', obterCanvas1);

///////////////////// cropper imagem 2 /////////////////////
const initCrop2 = () => {
    'use strict';
    const options = {
        aspectRatio: 3 / 5,
        preview: '.posCrop2'
    };
    cropper2 = new Cropper(preCrop2, options);
};
const carregarImg2 = (event) => {
    cropper2.destroy();
    cropper2 = null;
    let reader = new FileReader();
    reader.onload = () => {
        preCrop2.src = reader.result;
        initCrop2();
    }
    reader.readAsDataURL(event.target.files[0]);
};
const obterCanvas2 = () => {
    const canvas = cropper2.getCroppedCanvas({ maxWidth: 300, maxHeight: 500 });
    const urlcanvas = canvas.toDataURL('image/png');
    document.getElementById('hiddenCrop2').value = urlcanvas;
}
document.getElementById('btnCanvas2').addEventListener('click', obterCanvas2);


///////////////////// cropper imagem 3 /////////////////////
const initCrop3 = () => {
    'use strict';
    const options = {
        aspectRatio: 3 / 5,
        preview: '.posCrop3'
    };
    cropper3 = new Cropper(preCrop3, options);
};
const carregarImg3 = (event) => {
    cropper3.destroy();
    cropper3 = null;
    let reader = new FileReader();
    reader.onload = () => {
        preCrop3.src = reader.result;
        initCrop3();
    }
    reader.readAsDataURL(event.target.files[0]);
};
const obterCanvas3 = () => {
    const canvas = cropper3.getCroppedCanvas({ maxWidth: 300, maxHeight: 500 });
    const urlcanvas = canvas.toDataURL('image/png');
    document.getElementById('hiddenCrop3').value = urlcanvas;
}
document.getElementById('btnCanvas3').addEventListener('click', obterCanvas3);


///////////////////// cropper imagem 4 /////////////////////
const initCrop4 = () => {
    'use strict';
    const options = {
        aspectRatio: 3 / 5,
        preview: '.posCrop4'
    };
    cropper4 = new Cropper(preCrop4, options);
};
const carregarImg4 = (event) => {
    cropper4.destroy();
    cropper4 = null;
    let reader = new FileReader();
    reader.onload = () => {
        preCrop4.src = reader.result;
        initCrop4();
    }
    reader.readAsDataURL(event.target.files[0]);
};
const obterCanvas4 = () => {
    const canvas = cropper4.getCroppedCanvas({ maxWidth: 300, maxHeight: 500 });
    const urlcanvas = canvas.toDataURL('image/png');
    document.getElementById('hiddenCrop4').value = urlcanvas;
}
document.getElementById('btnCanvas4').addEventListener('click', obterCanvas4);