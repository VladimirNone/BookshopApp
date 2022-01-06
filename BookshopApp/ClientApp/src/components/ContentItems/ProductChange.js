import React, { Component, Fragment } from 'react';
import { Redirect } from 'react-router';
import { AppApiPaths } from '../Api-authorization/AppConstants';

export class ProductChange extends Component {

    constructor(props) {
        super(props);

        this.state = {
            productId: parseInt(props.match.params.id),
            product: null,
            authors: [],
            redirect: false,
            fileInputText: "Выбрать файл...",
            errors: [],
            badWords: ["наркотик","порно","пистолет"],
        }

        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        this.getProductAndAuthorsFromServer();
    }

    async handleSubmit(e) {
        e.preventDefault();

        const formData = new FormData(e.currentTarget);

        let nameOfProd = formData.get("name").toLowerCase();
        let descriptionOfProd = formData.get("description").toLowerCase();
        
        for (let i = 0; i < this.state.badWords.length; i++) {
            if (nameOfProd.includes(this.state.badWords[i]) || descriptionOfProd.includes(this.state.badWords[i])) {
                alert("Вы использовали запрещенное слово (" + this.state.badWords[i] + ")");

                return;
            }
        }

        let response = await fetch(AppApiPaths.ProductChange + "/" + this.state.productId, {
            method: 'PUT',
            body: formData,
        });

        if (!response.ok) {
            console.log(await response.json())
        }
        else {
            this.setState({ redirect: true });
        }
    }

    async getProductAndAuthorsFromServer() {
        let response = await fetch(AppApiPaths.ProductManipulateInfo + '/' + (this.state.productId), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            console.log(await response.json())
        }
        else {
            const answer = await response.json();
            this.setState({ product: answer.product, authors: answer.authors });
        }
    }

    render() {
        let product = this.state.product;
        let authors = this.state.authors;

        if (product == null)
            return (<div />)

        if (this.state.redirect)
            return (<Redirect to={'/'} />);

        return (
            <Fragment>
                <div className="row">
                    <div className="col">
                        <form className="" onSubmit={this.handleSubmit}>
                            <div className="mb-3 mt-3">
                                <h3>Форма для изменения продукта</h3>
                            </div>
                            <div className="mb-3">
                                <label>Название продукта</label>
                                <input name="name" type="text" className="form-control" placeholder="Название продукта" defaultValue={product.name}  />
                            </div>
                            <div className="row">
                                <div className="col-lg-3 mb-3">
                                    <label>Год публикации</label>
                                    <input name="yearOfRelease" type="number" className="form-control" placeholder="Год" defaultValue={product.yearOfRelease}  />
                                </div>
                                <div className="col-lg-9 mb-3">
                                    <label>Автор</label>
                                    <select name="authorId" className="custom-select" defaultValue={product.author.id} >
                                        <option value="0">Выбрать...</option>
                                        {authors.map((item, i) => <option value={item.id} key={i}>{item.firstName + " " + item.lastName}</option>)}
                                    </select>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-lg-6 mb-3">
                                    <label>Цена за экземпляр</label>
                                    <input name="price" type="text" className="form-control" placeholder="Цена" defaultValue={product.price?.toString().replace('.', ',')} />
                                </div>
                                <div className="col-lg-6 mb-3">
                                    <label>Количество экземпляров на складе</label>
                                    <input name="countInStock" type="text" className="form-control" placeholder="Количество" defaultValue={product.countInStock}  />
                                </div>
                            </div>
                            <div className="mb-3">
                                <label>Описание продукта</label>
                                <textarea name="description" className="form-control" placeholder="Опишите продукт в этом поле" defaultValue={product.description} ></textarea>
                            </div>
                            <div className="input-group mb-3">
                                <div className="custom-file">
                                    <input name="imageFile" className="custom-file-input" type="file" onChange={(e) => this.setState({ fileInputText: e.currentTarget.value.split('\\').pop() })} />
                                    <label className="custom-file-label">{this.state.fileInputText}</label>
                                </div>
                            </div>
                            <button className="btn btn-primary" type="submit">Отправить форму</button>
                        </form>
                    </div>
                </div>
            </Fragment>);
    }
}
