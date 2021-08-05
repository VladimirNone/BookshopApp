import React, { Component, Fragment } from 'react';
import { Redirect } from 'react-router';
import { AppApiPaths } from '../Api-authorization/AppConstants';

export class ProductManipulator extends Component {

    constructor(props) {
        super(props);

        this.state = {
            productId: parseInt(props.match.params.id),
            product: null,
            quantityProdForBuy: 1,
            redirect: false,
            access: false,
        }

        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        this.getProductAndAuthorsFromServer();
    }

    async getProductAndAuthorsFromServer() {
        let response = await fetch(AppApiPaths.ProductManipulator + '/' + (this.state.productId), {
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
            this.setState({ product: answer.product });
        }
    }

    async handleSubmit() {
        this.setState({ redirect: true });
    }

    render() {
        let product = this.state.product;
        let productIsNew = false;

        if (this.state.productId == 0) {
            product = {};
            productIsNew = true;
        }

        let conditionProduct = (isNotNull, isNull) => productIsNew ? isNotNull : isNull;

        if (product == null)
            return (<div/>)

        if (this.state.redirect)
            return (<Redirect to={'/'} />);

        return (
            <Fragment>
                <div className="row">
                    <div className="col">
                        <form className="" method="post" action={AppApiPaths.ProductManipulator + "/" + conditionProduct(0, this.state.productId)} encType="multipart/form-data">
                            <div className="mb-3 mt-3">
                                <h3>Форма для {conditionProduct("создания","изменения")} продукта</h3>
                            </div>
                            <div className="mb-3">
                                <label>Название продукта</label>
                                <input name="Name" type="text" className="form-control" placeholder="Название продукта" defaultValue={conditionProduct("", product.name)} />
                            </div>
                            <div className="row">
                                <div className="col-lg-3 mb-3">
                                    <label>Год публикации</label>
                                    <input name="YearOfRelease" type="number" className="form-control" placeholder="Год" defaultValue={conditionProduct("", product.yearOfRelease)}/>
                                </div>
                                <div className="col-lg-9 mb-3">
                                    <label>Автор</label>
                                    <select name="AuthorId" className="custom-select" defaultValue={conditionProduct("", 1)}>
                                        <option value="">Выбрать...</option>
                                        <option value="1">Один</option>
                                        <option value="2">Два</option>
                                        <option value="3">Три</option>
                                    </select>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-lg-6 mb-3">
                                    <label>Цена за экземпляр</label>
                                    <input name="Price" type="text" className="form-control" placeholder="Цена" defaultValue={conditionProduct("", (product.price === undefined ? "" : product.price.toString().replace('.', ',')))} />
                                </div>
                                <div className="col-lg-6 mb-3">
                                    <label>Количество экземпляров на складе</label>
                                    <input name="countInStock" type="text" className="form-control" placeholder="Количество" defaultValue={conditionProduct("", product.countInStock)}/>
                                </div>
                            </div>
                            <div className="mb-3">
                                <label>Описание продукта</label>
                                <textarea name="description" className="form-control" placeholder="Опишите продукт в этом поле" defaultValue={conditionProduct("", product.description)}></textarea>
                            </div>
                            <div className="input-group mb-3">
                                <div className="custom-file">
                                    <input name="ImageFile" type="file" className="custom-file-input" />
                                    <label className="custom-file-label">Выбрать файл...</label>
                                </div>
                            </div>
                            <button className="btn btn-primary" type="submit">Отправить форму</button>
                        </form>
                    </div>
                </div>
            </Fragment>);
    }
}
