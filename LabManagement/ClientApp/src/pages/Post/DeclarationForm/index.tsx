import { PlusOutlined, MinusCircleOutlined } from '@ant-design/icons';
import { Button, Card, Input, Form } from 'antd';
import { connect, Dispatch, FormattedMessage, IUser, UserModelState } from 'umi';
import React, { FC } from 'react';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { IChemical } from '@/models/entity';

const FormItem = Form.Item;

interface DeclarationFormProps {
  submitting: boolean;
  dispatch: Dispatch;
  currentUser?: IUser;
}

const DeclarationForm: FC<DeclarationFormProps> = (props) => {
  const { submitting, currentUser } = props;
  const [form] = Form.useForm();
  const submitFormLayout = {
    wrapperCol: {
      span: 8, offset: 8
    },
  };
  const rules = [{ required: true }]
  const onFinish = (values: { [key: string]: any }) => {
    const { dispatch } = props;
    const param = {
      form: {
        reason: values.reason,
        uid: currentUser?.userId,
        uname: currentUser?.userName,
        lid: currentUser?.labId,
      },
      chemicals: (values.chemicals).map((u: any)=>{
        return {
          ...u,
          labId: currentUser?.labId,
          amount: Number.parseInt(u.amount),
          unitprice: Number.parseFloat(u.unitprice),
        }
      })
    }
    dispatch({
      type: 'postDeclarationForm/submitDeclarationForm',
      payload: param,
    });
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };
  return (
    <PageHeaderWrapper>
      <Card bordered={false}>
        <Form
          hideRequiredMark
          style={{ marginTop: 8 }}
          labelAlign='left'
          labelCol={{ span: 3 }}
          form={form}
          name="basic"
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
        >
          <Card bordered={false}>
            <Form.Item name="reason" rules={rules} label='申请理由'>
              <Input />
            </Form.Item>
          </Card>
          <Form.List name="chemicals">
            {(fields, { add, remove }) => {
              /**
               * `fields` internal fill with `name`, `key`, `fieldKey` props.
               * You can extends this into sub field to support multiple dynamic fields.
               */
              return (
                <div>
                  {fields.map((field, index) => (
                    <Card
                      key={field.key}
                      style={{ marginBottom: 20 }}
                      title={'化学品编号' + (field.key + 1).toString()}
                      extra={<MinusCircleOutlined
                        className="dynamic-delete-button"
                        onClick={() => {
                          remove(field.name);
                        }} />
                      }
                    >
                      <Form.Item
                        name={[field.name, "name"]}
                        rules={rules}
                        label='化学品名称'
                      >
                        <Input placeholder="例：H2O" />
                      </Form.Item>
                      <Form.Item
                        name={[field.name, "amount"]}
                        rules={rules}
                        label='数量'
                      >
                        <Input/>
                      </Form.Item>
                      <Form.Item
                        name={[field.name, "unitmeasurement"]}
                        rules={rules}
                        label='单位'
                      >
                        <Input />
                      </Form.Item>
                      <Form.Item
                        name={[field.name, "labId"]}
                        label="实验室编号"
                      >
                        <Input disabled={true} defaultValue={currentUser?.labId}/>
                      </Form.Item>
                      <Form.Item
                        name={[field.name, "fname"]}
                        rules={rules}
                        label='生产厂家'
                      >
                        <Input placeholder="例：XXX有限公司" />
                      </Form.Item>
                      <Form.Item
                        name={[field.name, "unitprice"]}
                        rules={rules}
                        label='单价'
                      >
                        <Input/>
                      </Form.Item>
                    </Card>
                  ))}
                  <Form.Item>
                    <Button
                      type="dashed"
                      onClick={() => {
                        add();
                      }}
                      style={{ width: "100%" }}
                    >
                      <PlusOutlined /> 添加化学品
                    </Button>
                  </Form.Item>
                </div>
              );
            }}
          </Form.List>
          <FormItem {...submitFormLayout} style={{ marginTop: 32 }}>
            <Button type="primary" htmlType="submit" loading={submitting} style={{ width: '100%' }}>
              <FormattedMessage id="postanddeclarationform.form.submit" />
            </Button>
          </FormItem>
        </Form>
      </Card>
    </PageHeaderWrapper>
  );
};

export default connect(({ loading, user }: {
  loading: {
    effects: {
      [key: string]: boolean
    }
  },
  user: UserModelState;
}) => ({
  submitting: loading.effects['postDeclarationForm/submitRegularForm'],
  currentUser: user.currentUser
}))(DeclarationForm);
