import React, { Component, Fragment } from 'react';
import { AppApiPaths } from '../Api-authorization/AppConstants';

export class ProductCreate extends Component {

    constructor(props) {
        super(props);

        this.state = {
            authors: [],
            fileInputText: "Выбрать файл...",
        }
    }

    componentDidMount() {
        this.getDataFromServer();
    }

    async getDataFromServer() {
        let response = await fetch(AppApiPaths.ProductManipulateInfo, {
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
            this.setState({ authors: answer.authors });
        }
    }

    render() {
        let authors = this.state.authors;

        if (authors.length === 0)
            return (<div/>)

        return (
            <Fragment>
                <div className="row">
                    <div className="col">
                        <form className="" method="post" action={AppApiPaths.ProductCreate} encType="multipart/form-data">
                            <div className="mb-3 mt-3">
                                <h3>Форма для создания продукта</h3>
                            </div>
                            <div className="mb-3">
                                <label>Название продукта</label>
                                <input name="Name" type="text" className="form-control" placeholder="Название продукта"/>
                            </div>
                            <div className="row">
                                <div className="col-lg-3 mb-3">
                                    <label>Год публикации</label>
                                    <input name="YearOfRelease" type="number" className="form-control" placeholder="Год"/>
                                </div>
                                <div className="col-lg-9 mb-3">
                                    <label>Автор</label>
                                    <select name="AuthorId" className="custom-select">
                                        <option value="0">Выбрать...</option>
                                        {authors.map((item, i) => <option value={item.id} key={i}>{ item.firstName + " " + item.lastName}</option>)}
                                    </select>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-lg-6 mb-3">
                                    <label>Цена за экземпляр</label>
                                    <input name="Price" type="text" className="form-control" placeholder="Цена" />
                                </div>
                                <div className="col-lg-6 mb-3">
                                    <label>Количество экземпляров на складе</label>
                                    <input name="CountInStock" type="text" className="form-control" placeholder="Количество"/>
                                </div>
                            </div>
                            <div className="mb-3">
                                <label>Описание продукта</label>
                                <textarea name="Description" className="form-control" placeholder="Опишите продукт в этом поле"></textarea>
                            </div>
                            <div className="input-group mb-3">
                                <div className="custom-file">
                                    <input name="ImageFile" className="custom-file-input" type="file" onChange={(e) => this.setState({ fileInputText: e.currentTarget.value.split('\\').pop() })} />
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
