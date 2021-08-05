import React, { Component, Fragment } from 'react';
import { Redirect } from 'react-router';
import { AppApiPaths, AppPagePaths } from '../Api-authorization/AppConstants';
import authService from '../Api-authorization/AuthorizeService';
import { Link } from 'react-router-dom';

export class Product extends Component {

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
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    componentDidMount() {
        this.getProductFromServer();
        this.getPermission();
    }

    async getProductFromServer() {
        let response = await fetch(AppApiPaths.Product + '/' + (this.state.productId), {
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
            this.setState({ product: answer });
        }
    }

    async getPermission() {
        authService.checkPermission((res) => this.setState({ access: res.access }), (error) => console.log("error"));
    }

    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    async handleSubmit() {
        if (this.state.quantityProdForBuy > this.state.product.countInStock) {
            return alert("В наличии нет желаемого количества продуктов");
        }

        if (this.state.quantityProdForBuy < 1) {
            return alert("Нельзя заказывать отрицательное или нулевое количество продуктов");
        }

        var response = await fetch(AppApiPaths.AddToCart, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({ ProductId: this.state.product.id, Count: this.state.quantityProdForBuy })
        });
        
        if (!response.ok) {
            console.log("error")
        }
        else {
            this.setState({ redirect: true });
        }
    }

    render() {
        const product = this.state.product;

        if (product == null)
            return (<div />);

        if (this.state.redirect)
            return (<Redirect to={'/'} />);

        return (
            <Fragment>
                <div className="row" >
                    <img src={product.linkToImage} alt="no foto" width="100%" />
                </div>
                <div className="row mx-2 mt-3">
                    <div className="col">
                        <div className="">
                            <h4 className="">{product.name}</h4>
                            <h5 className="">Автор: {product.author.firstName + " " + product.author.lastName}</h5>
                            <div className="d-flex mb-3 mr-3">
                                <h5 className="flex-grow-1">В наличии: {product.countInStock}</h5>
                                <h5 className=""><b>{product.price}$</b></h5>
                            </div>
                            <p className="text-justify">{product.description}</p>
                        </div>
                        <div className="d-flex justify-content-end input-group ">
                            <form className="form-inline mr-3">
                                <div className="form-group mb-2">
                                    <p className="form-control-plaintext"><span>Количество</span></p>
                                </div>
                                <div className="form-group mx-3 mb-2">
                                    <input className="form-control" min="1" max="100" name="quantityProdForBuy" onChange={this.handleInputChange} value={this.state.quantityProdForBuy} type="number" />
                                </div>
                                <button type="button" className="btn btn-primary mb-2" onClick={() => this.handleSubmit()}>Добавить в корзину</button>
                            </form>
                        </div>
                        {this.state.access
                            && <div className=" mt-3">
                                <hr className="mb-3" />
                                <Link className="btn btn-primary btn-block" to={AppPagePaths.ProductManipulator + "/" + this.state.productId}>
                                    Изменить
                                </Link>
                            </div>}
                    </div>
                </div>
            </Fragment>);
    }
}
