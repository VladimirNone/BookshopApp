import React, { Component, Fragment } from 'react';
import { Searcher } from './MainPageComponents/Searcher';
import { Card } from './MainPageComponents/Card';
import { Pagination } from './MainPageComponents/Pagination';
import { ApplicationPaths } from '../Api-authorization/ApiConstants';

export class MainPage extends Component {
    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            products:[],
        }

    }

    componentDidMount() {
        this.getProductsFromServer();
    }

    async getProductsFromServer() {
        //The server pagination starts from zero
        let response = await fetch(ApplicationPaths.Products + '/' + (this.state.page - 1), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok)
        {
            console.log(await response.json())
        }
        else
        {
            this.setState({ products: await response.json() })
        }
    }

    render() {
        const prods = this.state.products;

        return (
            <Fragment>
                <Searcher />
                {prods.map((prod, i) => <Card key={i} product={prod} />)}
                <Pagination curPage={this.state.page} changePage={(newPage) => this.state.page == newPage && this.setState({ page: newPage })} />
            </Fragment>
        );
    }
}
